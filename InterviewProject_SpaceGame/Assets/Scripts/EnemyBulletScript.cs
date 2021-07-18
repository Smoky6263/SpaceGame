using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : PlayerBulletScript
{    
    private Transform player;

    private Vector2 direction;
    private float angle;

    private Quaternion angleAxis;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        direction = player.transform.position - transform.position;
        //Debug.DrawRay(transform.position, direction, Color.green);

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        //Debug.Log("Angle: " + angle);

        angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = angleAxis;
    }
}
