using UnityEngine;
using System;

public class ballmove : MonoBehaviour 
{

    public float speed = 800f;
    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    float moveHorizontal = 0f;
    float moveVertical = 0f;

    void Update() {
        if (Input.GetKeyDown(KeyCode.W)) moveVertical = 1;
        else if (Input.GetKeyDown(KeyCode.S)) moveVertical = -1;
        if (Input.GetKeyDown(KeyCode.A)) moveHorizontal = -1;
        else if (Input.GetKeyDown(KeyCode.D)) moveHorizontal = 1;

        if (Input.GetKeyUp(KeyCode.W)) moveVertical = 0;
        if (Input.GetKeyUp(KeyCode.A)) moveHorizontal = 0;
        if (Input.GetKeyUp(KeyCode.S)) moveVertical = 0;
        if (Input.GetKeyUp(KeyCode.D)) moveHorizontal = 0;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3 (moveVertical, 0.0f, -moveHorizontal);

        rb.velocity = (movement * speed * Time.deltaTime);
    }

}