using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMenu : MonoBehaviour {
    public void Engage() {
        SceneManager.LoadScene("Flight");
    }
}
