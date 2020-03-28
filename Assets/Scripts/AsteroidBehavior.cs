﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour {
    private const float Speed = 3.0f;
    
    // Update is called once per frame
    void Update() {
        Vector3 velocity = Vector3.down * Speed;
        transform.Translate(velocity * Time.deltaTime, Space.Self);

        if (transform.position.y < -50.0f) {
            Destroy(gameObject);
        }
    }
}