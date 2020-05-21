using Map;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners {
    public class AsteroidSpawner : MonoBehaviour {
    
        public GameObject asteroid;
    
        private const float MaxSpawnTime = 5.0f;

        private float _spawnTime;
        private float _timeRemaining;

        private void Start() {
            LocationData locData = SaveSystem.LoadLocation();
            _spawnTime = MaxSpawnTime / locData.difficulty;
            _timeRemaining = Random.Range(0.0f, _spawnTime);
        }

        // Update is called once per frame
        private void Update() {
            if (_timeRemaining > 0.0f) {
                _timeRemaining -= Time.deltaTime;
            }
            else {
                Instantiate(asteroid, new Vector3(Random.Range(Constants.FieldLeftBound, Constants.FieldRightBound), Constants.FieldTopSpawn, 0.0f), Quaternion.identity);
                _timeRemaining = Random.Range(0.0f, _spawnTime);
            }
        }
    }
}
