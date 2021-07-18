using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;

    private protected AudioSource audioSource;
    private protected Rigidbody2D rigidbody;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody2D>();        
    }

    private void Update()
    {
        rigidbody.AddForce(transform.up * speed);

        if (rigidbody.velocity.magnitude > maxSpeed)
            rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, maxSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SideBorder")
            Destroy(gameObject);
    }
}
