using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public Text gameOverText;
    public Text scoreText;

    private bool _isGameOver;
    private float _score;
    
    public void GameOver() {
        gameOverText.gameObject.SetActive(true);
        _isGameOver = true;
    }

    private void Start() {
        gameOverText.gameObject.SetActive(false);
        _isGameOver = false;
        _score = 0.0f;
    }

    private void Update() {
        if (_isGameOver && Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene("Map");
        }
        else if (!_isGameOver) {
            _score += Time.deltaTime;
            scoreText.text = ((int) _score).ToString();
        }
    }
}
