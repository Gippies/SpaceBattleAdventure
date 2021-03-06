﻿using UnityEngine;

namespace Player {
    public class BulletBehavior : MonoBehaviour {
    
        private const float Speed = 20.0f;
    
        // Update is called once per frame
        private void Update() {
            Vector3 velocity = Vector3.up * Speed;
            transform.Translate(velocity * Time.deltaTime, Space.Self);

            if (transform.position.y > Constants.FieldTopDeSpawn) {
                Destroy(gameObject);
            }
        }
    }
}
