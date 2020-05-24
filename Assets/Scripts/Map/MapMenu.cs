using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Map {
    public class MapMenu : MonoBehaviour {
        public Material lineMaterial;
        public GameObject toggleTemplate;
        public Button button;

        private Dictionary<Location, Toggle> _locToggleDict;
        private float _widthOffset = 4.0f;

        private void InitializeNewToggle(Location parent, Location location) {
            GameObject toggleGameObject = Instantiate(toggleTemplate, location.position, Quaternion.identity);
            toggleGameObject.transform.SetParent(transform);
            toggleGameObject.transform.localScale = Vector3.one;
            toggleGameObject.transform.localPosition = new Vector3(toggleGameObject.transform.localPosition.x, toggleGameObject.transform.localPosition.y, -51.0f);
            toggleGameObject.name = location.name;
            if (parent != null) {
                toggleGameObject.GetComponent<Toggle>().interactable = location != LocationManager.CurrentLocation && parent == LocationManager.CurrentLocation;
            }
            else {
                toggleGameObject.GetComponent<Toggle>().interactable = false;
            }
            
            toggleGameObject.GetComponent<Toggle>().isOn = false;
            toggleGameObject.GetComponentInChildren<Text>().text = location.name + " Difficulty: " + location.difficulty;
            _locToggleDict[location] = toggleGameObject.GetComponent<Toggle>();
        }

        private void DrawLine(Vector3 start, Vector3 end) {
            GameObject lineObject = new GameObject();
            lineObject.name = "Line";
            LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.positionCount = 2;
            lineRenderer.material = lineMaterial;

            Vector3[] positions = {new Vector3(start.x, start.y, 0.0f), new Vector3(end.x, end.y, 0.0f)};
            lineRenderer.SetPositions(positions);
        }

        private void RecursiveGenerateMap(Location location, int layer, int[] verticalPositions) {
            if (layer < 5) {
                for (int i = 0; i < Mathf.RoundToInt(Random.Range(0.6f, 2.4f)); i++) {
                    Location newLoc = new Location(
                        "Dest " + (i + 1), 
                        Mathf.Round(Random.Range(0.6f, 3.4f)), 
                        new Vector3(_locToggleDict[location].transform.position.x + _widthOffset, verticalPositions[layer], 0.0f), 
                        false
                    );
                    InitializeNewToggle(location, newLoc);
                    verticalPositions[layer]++;
                    DrawLine(_locToggleDict[location].transform.position, _locToggleDict[newLoc].transform.position);
                
                    _locToggleDict[newLoc].onValueChanged.AddListener(delegate {
                        SetDestination(newLoc);
                    });
                    location.destinations.Add(newLoc);
                    RecursiveGenerateMap(newLoc, layer + 1, verticalPositions);
                }
            }
        }

        private void GenerateMap() {
            Location firstLoc = new Location("Dest 0", 0.0f, new Vector3(-7.0f, 0.0f, 0.0f), true);
            InitializeNewToggle(null, firstLoc);
            LocationManager.RootLocation = firstLoc;
            
            _locToggleDict[firstLoc].onValueChanged.AddListener(delegate {
                SetDestination(firstLoc);
            });

            int[] verticalPosition = new int[5];
            RecursiveGenerateMap(firstLoc, 1, verticalPosition);
        }

        private void LoadMap(Location parent, Location location) {
            InitializeNewToggle(parent, location);
            
            _locToggleDict[location].onValueChanged.AddListener(delegate {
                SetDestination(location);
            });
            foreach (Location destination in location.destinations) {
                DrawLine(location.position, destination.position);
                LoadMap(location, destination);
            }
        }

        private void Start() {
            _locToggleDict = new Dictionary<Location, Toggle>();
            if (LocationManager.RootLocation == null) {
                GenerateMap();
            }
            else {
                LoadMap(null, LocationManager.RootLocation);
            }
        }

        public void ResetMap() {
            LocationManager.RootLocation = null;
            LocationManager.CurrentLocation = null;
            LocationManager.SelectedLocation = null;
            SceneManager.LoadScene("Map");
        }

        public void Engage() {
            SceneManager.LoadScene("Flight");
        }

        private void CheckToggled(Location location, Location changedLoc) {
            if (location != changedLoc && _locToggleDict[changedLoc].isOn && location != LocationManager.CurrentLocation) {
                _locToggleDict[location].isOn = false;
            }

            foreach (Location loc in location.destinations) {
                CheckToggled(loc, changedLoc);
            }
        }

        private void SetDestination(Location changedLoc) {
            if (_locToggleDict[changedLoc].isOn) {
                LocationManager.SelectedLocation = changedLoc;
            }
            CheckToggled(LocationManager.RootLocation, changedLoc);
            button.interactable = _locToggleDict[changedLoc].isOn;
        }
    }
}
