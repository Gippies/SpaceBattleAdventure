using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour {
    public GameObject asteroid;
    
    private const float MaxSpawnTime = 5.0f;
    private const float LeftBound = -20.0f;
    private const float RightBound = 20.0f;
    private const float TopBound = 15.0f;

    private float _timeRemaining;

    void Start() {
        _timeRemaining = Random.Range(0.0f, MaxSpawnTime);
    }

    // Update is called once per frame
    void Update() {
        if (_timeRemaining > 0.0f) {
            _timeRemaining -= Time.deltaTime;
        }
        else {
            Instantiate(asteroid, new Vector3(Random.Range(LeftBound, RightBound), TopBound, 0.0f), Quaternion.identity);
            _timeRemaining = Random.Range(0.0f, MaxSpawnTime);
        }
    }
}
