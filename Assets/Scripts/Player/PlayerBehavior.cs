using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
    
    public GameObject bullet;

    private const float Speed = 10.0f;
    private const float BottomBound = -8.0f;
    private const float TopBound = 8.0f;
    private const int InitHealth = 3;

    private int _health;

    private void UpdatePosition() {
        Vector3 velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * Speed;
        transform.Translate(velocity * Time.deltaTime);
        Vector3 pos = transform.position;
        transform.position = new Vector3(Mathf.Clamp(pos.x, Constants.FieldLeftBound, Constants.FieldRightBound), Mathf.Clamp(pos.y, BottomBound, TopBound), pos.z);
    }

    private void FireBullets() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Vector3 bulletPosition = new Vector3(transform.position.x, transform.position.y + bullet.transform.localScale.y * 2, transform.position.z);
            Instantiate(bullet, bulletPosition, Quaternion.identity);
        }
    }

    private void DestroyBehavior() {
        if (_health <= 0) {
            Destroy(gameObject);
            FindObjectOfType<GameManager>().GameOver();
        }
    }

    private void Start() {
        _health = InitHealth;
    }

    // Update is called once per frame
    private void Update() {
        UpdatePosition();
        FireBullets();
        DestroyBehavior();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Asteroid") || other.gameObject.CompareTag("Fighter") || other.gameObject.CompareTag("EBullet")) {
            _health--;
            Destroy(other.gameObject);
        }
    }
}
