using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public Text gameOverText;

    private bool _isGameOver;
    
    public void GameOver() {
        gameOverText.gameObject.SetActive(true);
        _isGameOver = true;
    }

    private void Start() {
        gameOverText.gameObject.SetActive(false);
        _isGameOver = false;
    }

    private void Update() {
        if (_isGameOver && Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene("Flight");
        }
    }
}
