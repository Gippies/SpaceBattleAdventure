﻿using UnityEngine;

namespace Enemies {
    public class FighterBehavior : MonoBehaviour {
    
        public GameObject bullet;
        public HealthBarBehavior healthBar;
    
        private const float Speed = 5.0f;
        private const float TimeBetweenBullets = 1.0f;
        private const int InitHealth = 2;

        private float _timeBetweenBullets;
        private int _health;

        private void UpdatePosition() {
            Vector3 velocity = Vector3.forward * Speed;
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

        private void DestroyBehavior() {
            if (transform.position.y < Constants.FieldBottomDeSpawn || _health <= 0) {
                Destroy(gameObject);
            }
        }

        private void Start() {
            _timeBetweenBullets = TimeBetweenBullets;
            _health = InitHealth;
            healthBar.SetMaxHealth(_health);
            healthBar.SetHealth(_health);
        }
    
        // Update is called once per frame
        private void Update() {
            UpdatePosition();
            FireBullets();
            DestroyBehavior();
        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Bullet")) {
                _health--;
                healthBar.SetHealth(_health);
                Destroy(other.gameObject);
            }
        }
    }
}
