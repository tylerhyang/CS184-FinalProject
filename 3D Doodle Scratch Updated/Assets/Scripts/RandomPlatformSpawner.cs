using System.Collections.Generic;
using UnityEngine;

public class RandomPlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public int numPlatformsToStart;
    public float minYDistance;
    public float maxYDistance;
    public float minXDistance;
    public float maxXDistance;
    public float minZDistance;
    public float maxZDistance;
    public float deleteDistance;
    public float perlinScaleX;
    public float perlinScaleY;
    public float perlinSeed;
    public int maxPlatformsPerLevel;
    public float perlinWeight;

    public Transform playerTransform;
    private Queue<GameObject> platformQueue;

    private Vector3 lastPlatformPosition;

    private void Start()
    {
        // playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Instantiate(platformPrefab, new Vector3(playerTransform.position.x, playerTransform.position.y - 1, playerTransform.position.z), Quaternion.identity);
        
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
        int numPlatformsGenerated = 0;

        for (int i = 0; i < numPlatformsToGenerate; i++)
        {
            Vector3 newPosition = lastPlatformPosition;
            float perlinValueX = Mathf.PerlinNoise(perlinSeed, 0);
            float perlinValueY = Mathf.PerlinNoise(0, perlinSeed);

            newPosition.x += (1 - perlinWeight) * Random.Range(minXDistance, maxXDistance) + perlinWeight * Mathf.Lerp(minXDistance, maxXDistance, perlinValueX);
            newPosition.y += (1 - perlinWeight) * Random.Range(minYDistance, maxYDistance) + perlinWeight * Mathf.Lerp(minYDistance, maxYDistance, perlinValueY);
            newPosition.z += Random.Range(minZDistance, maxZDistance);

            // Check for collisions
            Collider[] colliders = Physics.OverlapBox(newPosition, platformPrefab.transform.localScale / 2, Quaternion.identity);
            bool hasCollision = false;
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.tag != "Player") // Exclude the player from the collision check
                {
                    hasCollision = true;
                    break;
                }
            }

            if (!hasCollision)
            {
                GameObject newPlatform = Instantiate(platformPrefab, newPosition, Quaternion.identity);
                platformQueue.Enqueue(newPlatform);
                totalPlatformPositions += newPosition;
                numPlatformsGenerated++;
                perlinSeed += perlinScaleX;
            }

        }

        if (numPlatformsGenerated > 0)
        {
            lastPlatformPosition = totalPlatformPositions / numPlatformsGenerated;
        }
    }
}
