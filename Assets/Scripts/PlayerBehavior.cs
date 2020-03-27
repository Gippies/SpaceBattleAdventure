using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    public GameObject Bullet;

    private const float Speed = 10.0f;
    private const float LeftBound = -10.0f;
    private const float RightBound = 10.0f;
    private const float BottomBound = -8.0f;
    private const float TopBound = 8.0f;

    private void UpdatePosition() {
        Vector3 velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized * Speed;
        transform.Translate(velocity * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, LeftBound, RightBound), Mathf.Clamp(transform.position.y, BottomBound, TopBound), transform.position.z);
    }

    private void FireBullets() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Vector3 bulletPosition = new Vector3(transform.position.x, transform.position.y + Bullet.transform.localScale.y * 2, transform.position.z);
            Instantiate(Bullet, bulletPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update() {
        UpdatePosition();
        FireBullets();
    }
}
