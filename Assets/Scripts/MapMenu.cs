using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapMenu : MonoBehaviour {
    public List<Toggle> toggleList;
    public Button button;

    private Dictionary<Toggle, bool> _wasToggleActiveDict;

    private void Start() {
        _wasToggleActiveDict = new Dictionary<Toggle, bool>();
        foreach (Toggle t in toggleList) {
            _wasToggleActiveDict[t] = false;
        }
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
