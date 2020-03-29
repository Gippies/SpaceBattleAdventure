using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterSpawner : MonoBehaviour {
    
    public GameObject fighter;
    
    private const float MaxSpawnTime = 5.0f;
    private const float LeftBound = -20.0f;
    private const float RightBound = 20.0f;
    private const float TopBound = 15.0f;

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
            Instantiate(fighter, new Vector3(Random.Range(LeftBound, RightBound), TopBound, 0.0f), Quaternion.identity);
            _timeRemaining = Random.Range(0.0f, MaxSpawnTime);
        }
    }
}
