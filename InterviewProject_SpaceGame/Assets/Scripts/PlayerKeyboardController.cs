using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float rotationSpeed;

    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        PlayerController();
    }

    private void PlayerController()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.AddForce(transform.up * speed);

            if (rigidbody.velocity.magnitude > maxSpeed)
                rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, maxSpeed);
        }

        if (Input.GetKey(KeyCode.A))
            rigidbody.AddTorque(rotationSpeed);

        if (Input.GetKey(KeyCode.D))
            rigidbody.AddTorque(-rotationSpeed);
    }
}
