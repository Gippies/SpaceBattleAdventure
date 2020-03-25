using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    private const float Speed = 10.0f;
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        Vector3 velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized * Speed;
        transform.Translate(velocity * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, Settings.LeftBound, Settings.RightBound), Mathf.Clamp(transform.position.y, Settings.BottomBound, Settings.TopBound), transform.position.z);
    }
}
