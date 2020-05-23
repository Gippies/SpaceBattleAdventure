using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Map {
    public class MapMenu : MonoBehaviour {
        public GameObject toggleTemplate;
        public Button button;

        private Location _rootLocation;
        private Location _selectedLocation;
        private LineRenderer _lineRenderer;
        private int _highestId = 1;
        private float _widthOffset = 4.0f;

        private Location InitializeNewLocation(Vector3 position, bool isCurrent, string newToggleName, float difficulty) {
            Location location = new Location();
            GameObject toggleGameObject = Instantiate(toggleTemplate, position, Quaternion.identity);
            toggleGameObject.transform.SetParent(transform);
            toggleGameObject.transform.localScale = Vector3.one;
            toggleGameObject.transform.localPosition = new Vector3(toggleGameObject.transform.localPosition.x, toggleGameObject.transform.localPosition.y, -51.0f);
            toggleGameObject.name = newToggleName;
            toggleGameObject.GetComponent<Toggle>().interactable = !isCurrent;
            toggleGameObject.GetComponent<Toggle>().isOn = false;
            location.id = _highestId;
            _highestId++;
            location.toggle = toggleGameObject.GetComponent<Toggle>();
            location.difficulty = difficulty;
            location.isCurrent = isCurrent;
            toggleGameObject.GetComponentInChildren<Text>().text = newToggleName + " Difficulty: " + location.difficulty;

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

        private void GenerateMap() {
            Location firstLoc = InitializeNewLocation(new Vector3(-6.0f, 0.0f, 0.0f), true, "Dest 0", 0.0f);
            _rootLocation = firstLoc;
            
            firstLoc.toggle.onValueChanged.AddListener(delegate {
                SetDestination(firstLoc);
            });

            for (int i = 0; i < Mathf.RoundToInt(Random.Range(1.6f, 4.4f)); i++) {
                Location newLoc = InitializeNewLocation(
                    new Vector3(firstLoc.toggle.transform.position.x + _widthOffset, i - 1, 0.0f), 
                    false, 
                    "Dest " + (i + 1), 
                    Mathf.Round(Random.Range(0.6f, 3.4f))
                );
                DrawLine(firstLoc.toggle.transform.position, newLoc.toggle.transform.position);
                
                newLoc.toggle.onValueChanged.AddListener(delegate {
                    SetDestination(newLoc);
                });
                firstLoc.destinations.Add(newLoc);
            }
        }

        private Location LoadMap(LocationData locationData) {
            Vector3 locDataTransform = new Vector3(locationData.toggleVector3[0], locationData.toggleVector3[1], locationData.toggleVector3[2]);
            Location newLoc = InitializeNewLocation(locDataTransform, locationData.isCurrent, locationData.name, locationData.difficulty);
            
            newLoc.toggle.onValueChanged.AddListener(delegate {
                SetDestination(newLoc);
            });
            foreach (LocationData destinationData in locationData.destinations) {
                DrawLine(locDataTransform, new Vector3(destinationData.toggleVector3[0], destinationData.toggleVector3[1], destinationData.toggleVector3[2]));
                newLoc.destinations.Add(LoadMap(destinationData));
            }

            return newLoc;
        }

        private void Start() {
            if (!SaveSystem.MapExists()) {
                GenerateMap();
                SaveSystem.SaveMapData(_rootLocation);
            }
            else {
                _rootLocation = LoadMap(SaveSystem.LoadMapData().rootLocation);
            }
        }

        public void ResetMap() {
            SaveSystem.DeleteMapData();
            SceneManager.LoadScene("Map");
        }

        public void Engage() {
            SaveSystem.SaveLocation(_selectedLocation);
            SceneManager.LoadScene("Flight");
        }

        private void CheckToggled(Location location, Location changedLoc) {
            if (location != changedLoc && changedLoc.toggle.isOn && !location.isCurrent) {
                location.toggle.isOn = false;
            }

            foreach (Location loc in location.destinations) {
                CheckToggled(loc, changedLoc);
            }
        }

        private void SetDestination(Location changedLoc) {
            if (changedLoc.toggle.isOn) {
                _selectedLocation = changedLoc;
            }
            CheckToggled(_rootLocation, changedLoc);
            button.interactable = changedLoc.toggle.isOn;
        }
    }
}
