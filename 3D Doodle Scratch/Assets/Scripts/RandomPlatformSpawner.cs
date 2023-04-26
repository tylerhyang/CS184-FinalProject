using System.Collections.Generic;
using UnityEngine;

public class RandomPlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject staticPrefab;
    public GameObject patrolPrefab;
    public GameObject chasePrefab;
    public int numPlatformsToStart = 10;
    public float minYDistance = 2f;
    public float maxYDistance = 10f;
    public float minXDistance = -5f;
    public float maxXDistance = 5f;
    public float minZDistance = 10f;
    public float maxZDistance = 20f;
    public float deleteDistance = 10f;
    public float perlinScaleX = 0.01f;
    public float perlinScaleY = 0.01f;
    public float perlinSeed;
    public int maxPlatformsPerLevel;
    public int maxEnemiesPerLevel;
    public float perlinWeight = 0.2f;

    public Transform playerTransform;
    private Queue<GameObject> platformQueue;
    private Queue<GameObject> enemyQueue;

    private Vector3 lastPlatformPosition;
    private Vector3 lastEnemyPosition;

    private void Start()
    {
        // playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        platformQueue = new Queue<GameObject>();
        enemyQueue    = new Queue<GameObject>();

        lastPlatformPosition = new Vector3(0, 0, 0);
        lastEnemyPosition = new Vector3(0, 0, 0);
        perlinSeed = Random.Range(0, 10000);

        for (int i = 0; i < numPlatformsToStart; i++)
        {
            SpawnPlatform();
            SpawnEnemies();
        }
    }

    private void Update()
    {
        if (playerTransform.position.z - deleteDistance > platformQueue.Peek().transform.position.z)
        {
            GameObject platformToDelete = platformQueue.Dequeue();
            Destroy(platformToDelete);
            SpawnPlatform();
        }
    }

    private void SpawnPlatform()
    {
        int numPlatformsToGenerate = Random.Range(1, maxPlatformsPerLevel);
        Vector3 totalPlatformPositions = Vector3.zero;

        for (int i = 0; i < numPlatformsToGenerate; i++)
        {
            Vector3 newPosition = lastPlatformPosition;
            float perlinValueX = Mathf.PerlinNoise(perlinSeed, 0);
            float perlinValueY = Mathf.PerlinNoise(0, perlinSeed);

            newPosition.x += (1 - perlinWeight) * Random.Range(minXDistance, maxXDistance) + perlinWeight * Mathf.Lerp(minXDistance, maxXDistance, perlinValueX);
            newPosition.y += (1 - perlinWeight) * Random.Range(minYDistance, maxYDistance) + perlinWeight * Mathf.Lerp(minYDistance, maxYDistance, perlinValueY);
            newPosition.z += Random.Range(minZDistance, maxZDistance);

            GameObject newPlatform = Instantiate(platformPrefab, newPosition, Quaternion.identity);
            platformQueue.Enqueue(newPlatform);

            totalPlatformPositions += newPosition;
            perlinSeed += perlinScaleX;
        }

        if (numPlatformsToGenerate > 0)
        {
            lastPlatformPosition = totalPlatformPositions / numPlatformsToGenerate;
        }
    }

    private void SpawnEnemies()
    {
        int numEnemiesToGenerate = Random.Range(1, maxEnemiesPerLevel);
        Vector3 totalEnemyPositions = Vector3.zero;

        for (int i = 0; i < numEnemiesToGenerate; i++)
        {
            Vector3 newPosition = lastEnemyPosition;
            float perlinValueX = Mathf.PerlinNoise(perlinSeed, 0);
            float perlinValueY = Mathf.PerlinNoise(0, perlinSeed);

            newPosition.x += (1 - perlinWeight) * Random.Range(minXDistance, maxXDistance) + perlinWeight * Mathf.Lerp(minXDistance, maxXDistance, perlinValueX);
            newPosition.y += (1 - perlinWeight) * Random.Range(minYDistance, maxYDistance) + perlinWeight * Mathf.Lerp(minYDistance, maxYDistance, perlinValueY);
            newPosition.z += Random.Range(minZDistance, maxZDistance);

        //generate an enemy based on enemy CDF
            GameObject enemy = randomEnemyGen();
            GameObject newPlatform = Instantiate(enemy, newPosition, Quaternion.identity);
            platformQueue.Enqueue(newPlatform);

            totalEnemyPositions += newPosition;
            perlinSeed += perlinScaleX;
        }

        if (numEnemiesToGenerate > 0)
        {
            lastEnemyPosition = totalEnemyPositions / numEnemiesToGenerate;
        }
    }
    private GameObject randomEnemyGen()
    {
        float randomValue = Random.value;

        if (randomValue < 0.5f)
        {
            return staticPrefab;
        }
        else if (randomValue < 0.75f)
        {
            return patrolPrefab;
        }
        else
        {
        return chasePrefab;
        }
    }
}
