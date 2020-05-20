using System.Collections.Generic;
using UnityEngine;

namespace Map {
    public class Location : MonoBehaviour {
        public List<GameObject> destinations { get; } = new List<GameObject>();
    }
}
