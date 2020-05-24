using Map;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text missionCompleteText;
    public Text gameOverText;
    public Text scoreText;

    private bool _isGameOver;
    private bool _isComplete;
    private float _score;
    
    public void GameOver() {
        gameOverText.gameObject.SetActive(true);
        _isGameOver = true;
    }

    public void MissionComplete() {
        missionCompleteText.gameObject.SetActive(true);
        LocationManager.CurrentLocation = LocationManager.SelectedLocation;
        _isComplete = true;
    }

    private void Start() {
        gameOverText.gameObject.SetActive(false);
        missionCompleteText.gameObject.SetActive(false);
        _isGameOver = false;
        _isComplete = false;
        _score = 0.0f;
    }

    private void Update() {
        if ((_isGameOver || _isComplete) && Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene("Map");
        }
        else if (!_isGameOver && !_isComplete) {
            _score += Time.deltaTime;
            scoreText.text = (int) _score + "/60";
        }
        if (_score >= 60.0f && !_isComplete) {
            MissionComplete();
        }
    }
}
