using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private AsteroidsSpawnerScript asteroidsSpawnerScript;
    [SerializeField] private float speed, maxSpeed, rotationSpeed;
    [SerializeField] private bool bigAsteroidBool, middleAsteroidBool, littleAsteroidBool;
    [SerializeField] private GameObject explosionAudioSourceObj;
    public delegate void AsteroidKilledByPlayer_Delegate(int value);

    public static event AsteroidKilledByPlayer_Delegate AsteroidDeathByPlayer_Event;
        
    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponentInChildren<Rigidbody2D>();
    }

    private void Update()
    {
        rigidBody.AddForce(transform.up * speed);

        if (rigidBody.velocity.magnitude > maxSpeed)
            rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, maxSpeed);

        rigidBody.AddTorque(1f * rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Vector3 thisAsteroidPosition = gameObject.transform.position; 
            Vector3 bulletPosition = collision.gameObject.transform.position;


            Instantiate(explosionAudioSourceObj);
            Destroy(gameObject);
            Destroy(collision.gameObject);

            if (bigAsteroidBool)
            {
                asteroidsSpawnerScript.SpawnMiddleAsteroids(bulletPosition, thisAsteroidPosition);
                AsteroidDeathByPlayer_Event(20);

            }
            else if (middleAsteroidBool)
            {
                asteroidsSpawnerScript.SpawnLittleAsteroids(bulletPosition, thisAsteroidPosition);
                AsteroidDeathByPlayer_Event(50);

            }
            else
            {
                AsteroidDeathByPlayer_Event(100);

            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(explosionAudioSourceObj);
            Destroy(gameObject);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(explosionAudioSourceObj);
            Destroy(gameObject);

        }
    }
}
