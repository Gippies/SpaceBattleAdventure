using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterBehavior : MonoBehaviour {
    
    public GameObject bullet;
    
    private const float Speed = 3.0f;
    private const float TimeBetweenBullets = 1.0f;

    private float _timeBetweenBullets;

    private void UpdatePosition() {
        Vector3 velocity = Vector3.down * Speed;
        transform.Translate(velocity * Time.deltaTime, Space.Self);
    }

    private void FireBullets() {
        _timeBetweenBullets -= Time.deltaTime;
        if (_timeBetweenBullets < 0.0f) {
            Vector3 bulletPosition = new Vector3(transform.position.x, transform.position.y - bullet.transform.localScale.y * 2, transform.position.z);
            Instantiate(bullet, bulletPosition, Quaternion.identity);
            _timeBetweenBullets = TimeBetweenBullets;
        }
        
    }

    private void Start() {
        _timeBetweenBullets = TimeBetweenBullets;
    }
    
    // Update is called once per frame
    private void Update() {
        UpdatePosition();
        FireBullets();
        
        if (transform.position.y < -50.0f) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Bullet")) {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
