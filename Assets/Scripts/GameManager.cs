using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public Text gameOverText;
    
    public void GameOver() {
        gameOverText.gameObject.SetActive(true);
    }

    private void Start() {
        gameOverText.gameObject.SetActive(false);
    }
}
