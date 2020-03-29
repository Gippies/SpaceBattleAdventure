using UnityEngine;

public class EBulletBehavior : MonoBehaviour {
    
    private const float Speed = 20.0f;
    
    // Update is called once per frame
    private void Update() {
        Vector3 velocity = Vector3.down * Speed;
        transform.Translate(velocity * Time.deltaTime, Space.Self);

        if (transform.position.y < -50.0f) {
            Destroy(gameObject);
        }
    }
}
