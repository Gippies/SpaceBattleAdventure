using System;

namespace Map {
    [Serializable]
    public class MapData {
        public LocationData rootLocation;

        public MapData(Location location) {
            rootLocation = new LocationData(location);
        }
    }
}
