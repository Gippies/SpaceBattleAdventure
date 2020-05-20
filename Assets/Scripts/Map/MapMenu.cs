using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Map {
    public class MapMenu : MonoBehaviour {
        public GameObject toggleTemplate;
        public Button button;

        private Toggle _currentLocation;
        private List<Toggle> _toggles;
        private LineRenderer _lineRenderer;

        private GameObject InitializeNewToggle(Vector3 position, bool isCurrent, string newToggleName) {
            GameObject toggle = Instantiate(toggleTemplate, position, Quaternion.identity);
            toggle.transform.SetParent(transform);
            toggle.transform.localScale = Vector3.one;
            toggle.transform.localPosition = new Vector3(toggle.transform.localPosition.x, toggle.transform.localPosition.y, 0.0f);
            toggle.name = newToggleName;
            toggle.GetComponentInChildren<Text>().text = newToggleName;
            toggle.GetComponent<Toggle>().interactable = !isCurrent;
            toggle.GetComponent<Toggle>().isOn = isCurrent;
            toggle.AddComponent<Location>();

            return toggle;
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
            _toggles = new List<Toggle>();
            GameObject toggle = InitializeNewToggle(Vector3.zero, true, "Dest 0");
            _currentLocation = toggle.GetComponent<Toggle>();
            _toggles.Add(_currentLocation);
            
            for (int i = 0; i < Mathf.RoundToInt(Random.Range(0.6f, 3.4f)); i++) {
                GameObject newToggle = InitializeNewToggle(new Vector3(toggle.transform.position.x + toggle.transform.localScale.x * 2, i, 0.0f), false, "Dest " + (i + 1));
                toggle.GetComponent<Location>().destinations.Add(newToggle);
                DrawLine(toggle.transform.position, newToggle.transform.position);
                
                newToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate {
                    SetDestination(newToggle.GetComponent<Toggle>());
                });
                _toggles.Add(newToggle.GetComponent<Toggle>());
            }
        }

        public void Engage() {
            SceneManager.LoadScene("Flight");
        }

        public void SetDestination(Toggle changedToggle) {
            foreach (Toggle t in _toggles) {
                if (t != changedToggle && changedToggle.isOn && t != _currentLocation) {
                    t.isOn = false;
                }
            }
            button.interactable = changedToggle.isOn;
        }
    }
}
