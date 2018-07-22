using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

    private Rigidbody rb;
    private Camera camera;

    private string inputs = "";

    void Start()
    {
        camera = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = 0;
        float moveVertical = 0;

        if(Input.GetKeyDown(KeyCode.D))
        {
            moveHorizontal = 1;
            inputs += "D ";
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            moveHorizontal = -1;
            inputs += "A ";
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            moveVertical = 1;
            inputs += "W ";
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            moveVertical = -1;
            inputs += "S ";
        }

        //print(inputs);

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
        camera.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y + 5, rb.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("End"))
            Debug.Log("End");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit");
    }

}
