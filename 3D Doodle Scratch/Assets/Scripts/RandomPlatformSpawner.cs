using System.Collections.Generic;
using UnityEngine;

public class RandomPlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject glassPlatform;
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
    public float perlinWeight = 0.2f;

    public Transform playerTransform;
    private Queue<GameObject> platformQueue;

    private Vector3 lastPlatformPosition;

    private void Start()
    {
        // playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        platformQueue = new Queue<GameObject>();

        lastPlatformPosition = new Vector3(0, 0, 0);
        perlinSeed = Random.Range(0, 10000);

        for (int i = 0; i < numPlatformsToStart; i++)
        {
            SpawnPlatform();
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
            GameObject newPlatform;
            float platformTypeSeed = Random.Range(0, 10000);
            if (platformTypeSeed > 5000) {
                newPlatform = Instantiate(platformPrefab, newPosition, Quaternion.identity);
            } else {
                newPlatform = Instantiate(glassPlatform, newPosition, Quaternion.identity);
            }
            // GameObject newPlatform = Instantiate(platformPrefab, newPosition, Quaternion.identity);
            platformQueue.Enqueue(newPlatform);

            totalPlatformPositions += newPosition;
            perlinSeed += perlinScaleX;
        }

        if (numPlatformsToGenerate > 0)
        {
            lastPlatformPosition = totalPlatformPositions / numPlatformsToGenerate;
        }
    }
}
