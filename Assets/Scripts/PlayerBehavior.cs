using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
    
    public GameObject bullet;

    private const float Speed = 10.0f;
    private const float LeftBound = -20.0f;
    private const float RightBound = 20.0f;
    private const float BottomBound = -8.0f;
    private const float TopBound = 8.0f;

    private void UpdatePosition() {
        Vector3 velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized * Speed;
        transform.Translate(velocity * Time.deltaTime);
        Vector3 pos = transform.position;
        transform.position = new Vector3(Mathf.Clamp(pos.x, LeftBound, RightBound), Mathf.Clamp(pos.y, BottomBound, TopBound), pos.z);
    }

    private void FireBullets() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Vector3 bulletPosition = new Vector3(transform.position.x, transform.position.y + bullet.transform.localScale.y * 2, transform.position.z);
            Instantiate(bullet, bulletPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    private void Update() {
        UpdatePosition();
        FireBullets();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Asteroid") || other.gameObject.CompareTag("Fighter") || other.gameObject.CompareTag("EBullet")) {
            Destroy(gameObject);
        }
    }
}
