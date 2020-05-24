using System.Collections.Generic;
using UnityEngine;

namespace Map {
    public class Location {
        public string name { get; }
        public float difficulty { get; }
        public Vector3 position { get; }
        public List<Location> destinations { get; }

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
