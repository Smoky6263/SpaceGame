using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject enemyObj;
    [SerializeField] private Vector2[] spawnPosition;
    [SerializeField]  private float maxTimeToIncreaseEnemies;

    public int enemyCount;
    private int maxEnemyCount;

    private float spawnTime;
    private float spawnOffset;

    private float timeToIncreaseEnemies;

    private void Awake()
    {
        spawnOffset = 50f;
        spawnTime = Random.Range(20f, 40f);
        maxEnemyCount = 2;
        EnemyScript.DeathByAsteroid += EnemyDeath;
    }    

    private void Start()
    {
        timeToIncreaseEnemies = maxTimeToIncreaseEnemies;
    }

    private void Update()
    {
        SpawnEnemy();
    }
    private void FixedUpdate()
    {
        timeToIncreaseEnemies -= Time.deltaTime;

        if (timeToIncreaseEnemies < 0)
        {
            timeToIncreaseEnemies = maxTimeToIncreaseEnemies;
            maxEnemyCount++;
        }

    }

    private void SpawnEnemy()
    {
        spawnTime -= Time.deltaTime;

        if (spawnTime < 0 && enemyCount < 1)
        {
            enemyCount = maxEnemyCount;

            for (int i = 0; i < maxEnemyCount; i++)
            {

                spawnPosition = new Vector2[2];

                spawnPosition[0] = new Vector2((-Screen.width / 2 - spawnOffset), Random.Range((-Screen.height / 2) * 0.8f, (Screen.height / 2) * 0.8f));
                spawnPosition[1] = new Vector2((Screen.width / 2 + spawnOffset), Random.Range((-Screen.height / 2) * 0.8f, (Screen.height / 2) * 0.8f));

                int randomNumber = Random.Range(0, spawnPosition.Length);

                Instantiate(enemyObj, spawnPosition[randomNumber], Quaternion.Euler(0f,0f,0f));
            }

            spawnTime = Random.Range(20f, 40f);
        }

    }
    private void EnemyDeath(int value)
    {
        enemyCount -= value;
    }
}
