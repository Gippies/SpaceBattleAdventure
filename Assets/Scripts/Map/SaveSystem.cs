using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Map {
    public static class SaveSystem {
        
        private static string _selectedLocationPath = Application.persistentDataPath + "/location.space";
        private static string _mapPath = Application.persistentDataPath + "/map.space";
        
        public static void SaveStuff<T>(string path, T serializableData) {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Create);
            
            formatter.Serialize(stream, serializableData);
            stream.Close();
        }

        public static object LoadStuff(string path) {
            if (File.Exists(path)) {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                object data = formatter.Deserialize(stream);
                stream.Close();

                return data;
            }
            Debug.LogError("Save file not found in " + path);
            return null;
        }

        public static bool MapExists() {
            return File.Exists(_mapPath);
        }

        public static void DeleteMapData() {
            File.Delete(_mapPath);
        }
        
        public static void SaveMapData(Location location) {
            MapData data = new MapData(location);
            SaveStuff(_mapPath, data);
        }
        
        public static void SaveLocation(Location location) {
            LocationData data = new LocationData(location);
            SaveStuff(_selectedLocationPath, data);
        }

        public static MapData LoadMapData() {
            return (MapData) LoadStuff(_mapPath);
        }

        public static LocationData LoadLocation() {
            return (LocationData) LoadStuff(_selectedLocationPath);
        }
    }
}