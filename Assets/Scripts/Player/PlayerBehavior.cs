using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
    
    public GameObject bullet;
    public HealthBarBehavior healthBar;

    private const float Speed = 10.0f;
    private const int InitHealth = 3;

    private int _health;

    private void UpdatePosition() {
        Vector3 velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * Speed;
        transform.Translate(velocity * Time.deltaTime);
        
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPos.x < 0.0) {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, viewportPos.y, viewportPos.z));
        }
        else if (viewportPos.x > 1.0) {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, viewportPos.y, viewportPos.z));;
        }
        
        if (viewportPos.y < 0.0) {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(viewportPos.x, 0.0f, viewportPos.z));
        }
        else if (viewportPos.y > 1.0) {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(viewportPos.x, 1.0f, viewportPos.z));;
        }
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
        healthBar.SetHealth(_health);
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
            healthBar.SetHealth(_health);
            Destroy(other.gameObject);
        }
    }
}
