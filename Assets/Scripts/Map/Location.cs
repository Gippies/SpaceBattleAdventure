using System.Collections.Generic;
using UnityEngine.UI;

namespace Map {
    public class Location {
        public int id { get; set; }
        public float difficulty { get; set; }
        public bool isCurrent { get; set; }
        public Toggle toggle { get; set; }
        public List<Location> destinations { get; set; }

        public Location() {
            destinations = new List<Location>();
        }
    }
}
