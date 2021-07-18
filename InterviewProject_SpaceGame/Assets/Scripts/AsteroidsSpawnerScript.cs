using UnityEngine;

public class AsteroidsSpawnerScript : MonoBehaviour
{
    [SerializeField] GameObject bigAsteroid, middleAsteroid, LittleAsteroid;
    [SerializeField] private int maximumAsteroidsCount = 2;

    private int allAsteroidsCount;

    private int big, middle, little;

    private float asteroidsSpawnTime;
    private float maxAsteroidSpawnTime;


    private void Start()
    {
        SpawnBigAsteroids();
        maxAsteroidSpawnTime = 2f;
        asteroidsSpawnTime = maxAsteroidSpawnTime;
    }

    private void Update()
    {
        CheckAsteroidsCount();
    }

    private void SpawnBigAsteroids()
    {
        Vector2[] randomPosition = new Vector2[4];

        for (int i = 0; i < maximumAsteroidsCount; i++)
        {
            randomPosition[0] = new Vector2(Random.Range(-600f, 600f), 450f);
            randomPosition[1] = new Vector2(Random.Range(-600f, 600f), -450f);
            randomPosition[2] = new Vector2(-600f, Random.Range(-450f, 450f));
            randomPosition[3] = new Vector2(600f, Random.Range(-450f, 450f));
            float _random_z_Angle = Random.Range(0f, 360f);
        
            int randomNumber = Random.Range(0, randomPosition.Length);
            Instantiate(bigAsteroid, randomPosition[randomNumber], Quaternion.Euler(0f, 0f, _random_z_Angle));
        }

        maximumAsteroidsCount++;
        asteroidsSpawnTime = maxAsteroidSpawnTime;

    }

    public void SpawnMiddleAsteroids(Vector3 _targetPosition, Vector3 _asteroidPosition)
    {
        Vector3 offset = new Vector3(75, 0f);

        Vector3 direction = _targetPosition - _asteroidPosition;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;


        Quaternion angleAxisLeft = Quaternion.AngleAxis(angle + 45f, Vector3.forward);
        Quaternion angleAxisRight = Quaternion.AngleAxis(angle - 45f, Vector3.forward);

        Instantiate(middleAsteroid, _asteroidPosition + offset, angleAxisLeft);
        Instantiate(middleAsteroid, _asteroidPosition + offset, angleAxisRight);

    }

    internal void SpawnLittleAsteroids(Vector3 _targetPosition, Vector3 _asteroidPosition)
    {
        Vector3 offset = new Vector3(50, 0f);

        Vector3 direction = _targetPosition - _asteroidPosition;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;

        Quaternion angleAxisLeft = Quaternion.AngleAxis(angle + 45f, Vector3.forward);
        Quaternion angleAxisRight = Quaternion.AngleAxis(angle - 45f, Vector3.forward);

        Instantiate(LittleAsteroid, _asteroidPosition + offset, angleAxisLeft);
        Instantiate(LittleAsteroid, _asteroidPosition + offset, angleAxisRight);

    }

    private void CheckAsteroidsCount()
    {
        big = GameObject.FindGameObjectsWithTag("BigAsteroid").Length;
        middle = GameObject.FindGameObjectsWithTag("MiddleAsteroid").Length;
        little = GameObject.FindGameObjectsWithTag("LittleAsteroid").Length;

        allAsteroidsCount = big + middle + little;

        Debug.Log(allAsteroidsCount);

        if (allAsteroidsCount == 0)
        {
            asteroidsSpawnTime -= Time.deltaTime;

            if (asteroidsSpawnTime < 0)
                SpawnBigAsteroids();

        }
    }


}
