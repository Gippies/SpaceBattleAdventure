using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Map {
    public class MapMenu : MonoBehaviour {
        public Toggle initialToggle;
        public List<Toggle> toggleList;
        public Button button;

        private Dictionary<Toggle, bool> _wasToggleActiveDict;
        private LineRenderer _lineRenderer;

        private void Start() {
            _wasToggleActiveDict = new Dictionary<Toggle, bool>();
            foreach (Toggle t in toggleList) {
                _wasToggleActiveDict[t] = false;
            }
            DrawLines();
        }

        private void DrawLines() {
            GameObject lineObject = new GameObject("Line");
            _lineRenderer = lineObject.AddComponent<LineRenderer>();
            _lineRenderer.startWidth = 0.1f;
            _lineRenderer.endWidth = 0.1f;
            _lineRenderer.positionCount = toggleList.Count + 1;

            List<Vector3> positionList = new List<Vector3> {
                new Vector3(initialToggle.gameObject.transform.position.x, initialToggle.gameObject.transform.position.y, 0.0f)
            };
            for (int i = 0; i < toggleList.Count; i++) {
                positionList.Add(new Vector3(toggleList[i].gameObject.transform.position.x, toggleList[i].gameObject.transform.position.y, 0.0f));
            }
        
            _lineRenderer.SetPositions(positionList.ToArray());
        }
    
        public void Engage() {
            SceneManager.LoadScene("Flight");
        }

        public void SetDestination(bool b) {
            bool setActive = false;
            foreach (Toggle t in toggleList) {
                if (b && t.isOn && _wasToggleActiveDict[t]) {
                    t.isOn = false;
                    _wasToggleActiveDict[t] = false;
                }
                else if (b && t.isOn && !_wasToggleActiveDict[t]) {
                    _wasToggleActiveDict[t] = true;
                }
                else if (!b && !t.isOn && _wasToggleActiveDict[t]) {
                    _wasToggleActiveDict[t] = false;
                }
                if (t.isOn) {
                    setActive = true;
                }
            }
            button.interactable = setActive;
        }
    }
}
