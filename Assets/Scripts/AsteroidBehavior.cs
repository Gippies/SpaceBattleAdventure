using UnityEngine;

public class AsteroidBehavior : MonoBehaviour {
    
    private const float Speed = 3.0f;

    private void UpdatePosition() {
        Vector3 velocity = Vector3.down * Speed;
        transform.Translate(velocity * Time.deltaTime, Space.Self);
    }

    private void DestroyBehavior() {
        if (transform.position.y < Constants.FieldBottomDeSpawn) {
            Destroy(gameObject);
        }
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
