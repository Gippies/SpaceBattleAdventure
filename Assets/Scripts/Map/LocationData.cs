using System;
using System.Collections.Generic;

namespace Map {
    [Serializable]
    public class LocationData {
        public int id;
        public string name;
        public bool isCurrent;
        public float difficulty;
        public float[] toggleVector3;
        public List<LocationData> destinations;

        public LocationData(Location location) {
            id = location.id;
            name = location.toggle.name;
            difficulty = location.difficulty;
            isCurrent = location.isCurrent;
            toggleVector3 = new [] { location.toggle.transform.position.x, location.toggle.transform.position.y, location.toggle.transform.position.z };
            destinations = new List<LocationData>();
            foreach (Location destination in location.destinations) {
                destinations.Add(new LocationData(destination));
            }
        }
    }
}