using System;

namespace Map {
    [Serializable]
    public class LocationData {
        public int id;
        public float difficulty;

        public LocationData(Location location) {
            id = location.id;
            difficulty = location.difficulty;
        }
    }
}