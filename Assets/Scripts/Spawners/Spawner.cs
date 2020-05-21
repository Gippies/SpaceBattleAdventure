using Map;
using UnityEngine;

namespace Spawners {
    public class Spawner : MonoBehaviour {
    
        public GameObject objectToSpawn;
    
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
                Instantiate(objectToSpawn, new Vector3(Random.Range(Constants.FieldLeftBound, Constants.FieldRightBound), Constants.FieldTopSpawn, 0.0f), Quaternion.Euler(new Vector3(90, 0, 180)));
                _timeRemaining = Random.Range(0.0f, _spawnTime);
            }
        }
    }
}
