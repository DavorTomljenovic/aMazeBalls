using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

    private Rigidbody rb;
    private Camera camera;

    void Start()
    {
        camera = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
        camera.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y + 5, rb.transform.position.z);
    }
}
