using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    float speed = 3f;
    float acceleration = 100f;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
            inputVector.x -= 1;
        if (Input.GetKey(KeyCode.D))
            inputVector.x += 1;
        if (Input.GetKey(KeyCode.W))
            inputVector.y += 1;
        if (Input.GetKey(KeyCode.S))
            inputVector.y -= 1;
        //print(inputVector);

        rb.velocity = Vector2.MoveTowards(rb.velocity, speed * inputVector.normalized, Time.deltaTime * acceleration);
    }
}
