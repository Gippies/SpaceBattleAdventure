using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Map {
    public static class SaveSystem {
        
        private static string _path = Application.persistentDataPath + "/location.space";
        
        public static void SaveLocation(Location location) {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(_path, FileMode.Create);
            
            LocationData data = new LocationData(location);
            
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static LocationData LoadLocation() {
            if (File.Exists(_path)) {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(_path, FileMode.Open);

                LocationData data = formatter.Deserialize(stream) as LocationData;
                stream.Close();

                return data;
            }
            Debug.LogError("Save file not found in " + _path);
            return null;
        }
    }
}