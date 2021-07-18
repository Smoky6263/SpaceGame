using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private GameObject enemyBullet, explosionAudioPrefab;
    [SerializeField] private int speed;
    [SerializeField] private float maxSpeed;

    public delegate void EnemyDeathByAsteroid_Delegate(int value);
    public delegate void EnemyKilledByPlayer_Event(int value);

    public static event EnemyDeathByAsteroid_Delegate DeathByAsteroid;
    public static event EnemyKilledByPlayer_Event TakeScoreFromEnemy;

    private Rigidbody2D rigidBody;
    private bool driveLeft;

    private float timeToShoot;
    private float maxTimeToShoot;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        driveLeft = Random.Range(0, 100) > 50f ? true : false;

        maxTimeToShoot = Random.Range(2f, 5f);
        timeToShoot = maxTimeToShoot;
    }

    void Update()
    {
        EnemyController();
        ShootPlayer();
    }

    
    private void EnemyController()
    {
        rigidBody.AddForce(driveLeft ? -transform.right * speed : transform.right * speed);

        if (rigidBody.velocity.magnitude > maxSpeed)
            rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, maxSpeed);
    }

    private void ShootPlayer()
    {
        timeToShoot -= Time.deltaTime;

        if (timeToShoot < 0)
        {
            Instantiate(enemyBullet, transform.position, Quaternion.identity);
            maxTimeToShoot = Random.Range(2f, 5f);
            timeToShoot = maxTimeToShoot;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BigAsteroid" || collision.gameObject.tag == "MiddleAsteroid" || collision.gameObject.tag == "LittleAsteroid")
        {
            Instantiate(explosionAudioPrefab);
            Destroy(gameObject);
            DeathByAsteroid(1);
        }

        if (collision.gameObject.tag == "PlayerBullet")
        {
            Instantiate(explosionAudioPrefab);
            Destroy(collision.gameObject);
            Destroy(gameObject);
            DeathByAsteroid(1);
            TakeScoreFromEnemy(200);
        }
    }
    
}
