using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Map {
    public class MapMenu : MonoBehaviour {
        public GameObject toggleTemplate;
        public Button button;

        private Location _currentLocation;
        private Location _selectedLocation;
        private List<Location> _locations;
        private LineRenderer _lineRenderer;
        private float _widthOffset = 4.0f;

        private Location InitializeNewLocation(Vector3 position, bool isCurrent, string newToggleName, float difficulty) {
            Location location = new Location();
            GameObject toggle = Instantiate(toggleTemplate, position, Quaternion.identity);
            toggle.transform.SetParent(transform);
            toggle.transform.localScale = Vector3.one;
            toggle.transform.localPosition = new Vector3(toggle.transform.localPosition.x, toggle.transform.localPosition.y, -100.0f);
            toggle.name = newToggleName;
            toggle.GetComponent<Toggle>().interactable = !isCurrent;
            toggle.GetComponent<Toggle>().isOn = isCurrent;
            location.toggle = toggle.GetComponent<Toggle>();
            location.difficulty = difficulty;
            toggle.GetComponentInChildren<Text>().text = newToggleName + " Difficulty: " + location.difficulty;

            return location;
        }

        private void DrawLine(Vector3 start, Vector3 end) {
            GameObject lineObject = new GameObject();
            _lineRenderer = lineObject.AddComponent<LineRenderer>();
            _lineRenderer.startWidth = 0.1f;
            _lineRenderer.endWidth = 0.1f;
            _lineRenderer.positionCount = 2;

            Vector3[] positions = {new Vector3(start.x, start.y, 0.0f), new Vector3(end.x, end.y, 0.0f)};
            _lineRenderer.SetPositions(positions);
        }

        private void Start() {
            _locations = new List<Location>();
            Location firstLoc = InitializeNewLocation(Vector3.zero, true, "Dest 0", 0.0f);
            _currentLocation = firstLoc;
            _locations.Add(_currentLocation);
            
            for (int i = 0; i < Mathf.RoundToInt(Random.Range(0.6f, 3.4f)); i++) {
                Location newLoc = InitializeNewLocation(
                    new Vector3(firstLoc.toggle.transform.position.x + _widthOffset, i, 0.0f), 
                    false, 
                    "Dest " + (i + 1), 
                    i + 1
                    );
                DrawLine(firstLoc.toggle.transform.position, newLoc.toggle.transform.position);
                
                newLoc.toggle.onValueChanged.AddListener(delegate {
                    SetDestination(newLoc);
                });
                _locations.Add(newLoc);
            }
        }

        public void Engage() {
            SaveSystem.SaveLocation(_selectedLocation);
            SceneManager.LoadScene("Flight");
        }

        private void SetDestination(Location changedLoc) {
            if (changedLoc.toggle.isOn) {
                _selectedLocation = changedLoc;
            }
            foreach (Location l in _locations) {
                if (l != changedLoc && changedLoc.toggle.isOn && l != _currentLocation) {
                    l.toggle.isOn = false;
                }
            }
            button.interactable = changedLoc.toggle.isOn;
        }
    }
}
