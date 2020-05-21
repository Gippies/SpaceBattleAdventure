using System;

namespace Map {
    [Serializable]
    public class LocationData {
        public float difficulty;

        public LocationData(Location location) {
            difficulty = location.difficulty;
        }
    }
}