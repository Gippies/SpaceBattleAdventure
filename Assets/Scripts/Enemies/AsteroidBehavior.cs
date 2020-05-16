using UnityEngine;

namespace Enemies {
    public class AsteroidBehavior : MonoBehaviour {
    
        private const float Speed = 3.0f;
        private const float RotationSpeed = 80.0f;

        private Vector3 _rotationDirection;

        private void UpdatePosition() {
            Vector3 velocity = Vector3.down * Speed;
            Vector3 rotationVelocity = _rotationDirection * RotationSpeed;
            transform.Translate(velocity * Time.deltaTime, Space.World);
            transform.Rotate(rotationVelocity * Time.deltaTime);
        }

        private void DestroyBehavior() {
            if (transform.position.y < Constants.FieldBottomDeSpawn) {
                Destroy(gameObject);
            }
        }

        private void Start() {
            _rotationDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        }

        private void Update() {
            UpdatePosition();
            DestroyBehavior();
        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Bullet")) {
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
        }
    }
}
