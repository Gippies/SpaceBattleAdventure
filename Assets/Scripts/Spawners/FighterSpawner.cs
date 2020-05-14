using UnityEngine;

public class FighterSpawner : MonoBehaviour {
    
    public GameObject fighter;
    
    private const float MaxSpawnTime = 5.0f;

    private float _timeRemaining;

    private void Start() {
        _timeRemaining = Random.Range(0.0f, MaxSpawnTime);
    }

    // Update is called once per frame
    private void Update() {
        if (_timeRemaining > 0.0f) {
            _timeRemaining -= Time.deltaTime;
        }
        else {
            Instantiate(fighter, new Vector3(Random.Range(Constants.FieldLeftBound, Constants.FieldRightBound), Constants.FieldTopSpawn, 0.0f), Quaternion.Euler(new Vector3(90, 0, 180)));
            _timeRemaining = Random.Range(0.0f, MaxSpawnTime);
        }
    }
}
