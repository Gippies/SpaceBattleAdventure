using System;
using System.Collections.Generic;

namespace Map {
    [Serializable]
    public class MapData {
        public List<LocationData> locations;

        public MapData(List<Location> locs) {
            locations = new List<LocationData>();
            foreach (Location l in locs) {
                locations.Add(new LocationData(l));
            }
        }
    }
}
