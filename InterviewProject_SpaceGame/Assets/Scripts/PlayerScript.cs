using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public delegate void DecrementLive_Delegate(int value);

    public static event DecrementLive_Delegate decrementLive_Event;

    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] [Range(0, 1)] private float mouseRotationSpeed;


    [SerializeField] private GameObject bullet, explosionAudio;

    private AudioSource audioSource;
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;

    private Vector3 bulletOffset;

    private float maxRealoadingTime;
    private float reloadingTime;

    private float changeMaskTime;
    private float maxChangeMAskTime;

    float timeForRevival;
    float maxTimeForRevival = 3f;

    private bool isAlive;
    private bool readyToShoot;
    public bool isKeybordRotationControl;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody2D>();

        maxRealoadingTime = 0.33f;
        reloadingTime = maxRealoadingTime;

        bulletOffset = new Vector3(0f, 10f, 0.04f);
        timeForRevival = maxTimeForRevival;
        isAlive = true;

        maxChangeMAskTime = 0.25f;
        changeMaskTime = maxChangeMAskTime;

        GameMenuScript.controlChanged_Event += SetControl;
    }

    private void Update()
    {
        PlayerController();
        CheckPlayerIsAlive();

        if (!readyToShoot)
            PlayerReloading();
    }

    private void CheckPlayerIsAlive()
    {

        if (!isAlive)
        {

            PlayerMasking();

            timeForRevival -= Time.deltaTime;

            if (timeForRevival < 0)
            {
                isAlive = true;
                timeForRevival = maxTimeForRevival;
                spriteRenderer.enabled = true;
            }
        }
    }

    private void PlayerMasking()
    {
        changeMaskTime -= Time.deltaTime;

        if (changeMaskTime < 0)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            changeMaskTime = maxChangeMAskTime;
        }
    }

    private void PlayerController()
    {

        if (isKeybordRotationControl)
            KeyboardControl();
        else
            KeyboardAndMouseControl();

    }

    private void SetControl(bool value)
    {
        isKeybordRotationControl = value;
    }

    private void KeyboardControl()
    {
        if (Input.GetKeyDown(KeyCode.W))
            audioSource.Play();
        else if (Input.GetKeyUp(KeyCode.W))
            audioSource.Pause();

        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.AddForce(transform.up * speed);

            if (rigidbody.velocity.magnitude > maxSpeed)
                rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, maxSpeed);

        }
        
        if (Input.GetKeyDown(KeyCode.Space))
            TryToShoot();

        KeyboardRotationController();
    }

    private void KeyboardAndMouseControl()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Mouse1))
            audioSource.Play();
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.Mouse1))
            audioSource.Pause();

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Mouse1))
        {
            rigidbody.AddForce(transform.up * speed);

            if (rigidbody.velocity.magnitude > maxSpeed)
                rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, maxSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
            TryToShoot();

        MouseRotationController();

    }

    private void KeyboardRotationController()
    {
        if (Input.GetKey(KeyCode.A))
            rigidbody.AddTorque(rotationSpeed);

        if (Input.GetKey(KeyCode.D))
            rigidbody.AddTorque(-rotationSpeed);
    }

    private void MouseRotationController()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0) - transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, angleAxis, mouseRotationSpeed);
    }
    private void TryToShoot()
    {
        if (!readyToShoot)
        {
            return;
        }
        else
            PlayerShoot();

    }
    private void PlayerShoot()
    {
        Instantiate(bullet, transform.position + bulletOffset, transform.rotation);
        readyToShoot = false;
    }

    private void PlayerReloading()
    {        
        reloadingTime -= Time.deltaTime;

        if (reloadingTime <= 0)
        {
            readyToShoot = true;
            reloadingTime = maxRealoadingTime;
        }
    }    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isAlive)
        {
            isAlive = false;
            decrementLive_Event(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAlive) 
        {
            if (collision.gameObject.CompareTag("EnemyBullet"))
            {
                isAlive = false;
                decrementLive_Event(1);
                Instantiate(explosionAudio);
                Destroy(collision.gameObject);
            }
        }
    }
}
