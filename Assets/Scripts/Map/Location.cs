using System.Collections.Generic;
using UnityEngine;

namespace Map {
    public class Location {
        public string name { get; set; }
        public float difficulty { get; set; }
        public Vector3 position { get; set; }
        public List<Location> destinations { get; set; }

        public Location(string name, float difficulty, Vector3 position, bool isCurrent) {
            this.name = name;
            this.difficulty = difficulty;
            this.position = position;
            if (isCurrent) {
                LocationManager.CurrentLocation = this;
            }
            destinations = new List<Location>();
        }
    }
}
