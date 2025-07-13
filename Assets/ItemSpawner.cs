using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Transform playerForklift;
    public Transform enemyForklift;
    public float exclusionRadius = 5f; // how far away obstacles must be

    public ScoreManager scoreManager;

    // Array to hold the prefabs for obstacles
    public GameObject[] obstaclePrefabs;
    
    // Number of items to spawn
    public int itemsToSpawn = 10;

    public bool has_spawned = false;
    
    // Arena boundaries for spawn positions
    public Vector3 spawnArea;

    // Start is called before the first frame update
    void Start()
    {
        // Call the function to spawn items at the start
        SpawnObstacles();
        
    }
    // void Update()
    // {
    //     // Check if a round is not active, and spawn new obstacles when a round is over

    //     if (scoreManager.roundActive && !has_spawned) 
    //     {
    //         SpawnObstacles();
    //         has_spawned = true;
    //     }
    //     if (!scoreManager.roundActive){

    //         has_spawned = false;
    //     }
    // }
    // Function to spawn obstacles
    public void SpawnObstacles()
    {
        // Clear any existing obstacles (optional)
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Spawn 'itemsToSpawn' random obstacles
        for (int i = 0; i < itemsToSpawn; i++)
        {
            Vector3 randomPos;
            int attempts = 0;
            const int maxAttempts = 100;

            do
            {
                randomPos = new Vector3(
                    Random.Range(8, spawnArea.x),
                    0,
                    Random.Range(11, spawnArea.z)
                );
                attempts++;
            }
            while (
                (Vector3.Distance(randomPos, playerForklift.position) < exclusionRadius ||
                Vector3.Distance(randomPos, enemyForklift.position) < exclusionRadius)
                && attempts < maxAttempts
            );

            // Spawn only if a valid position was found
            if (attempts < maxAttempts)
            {
                GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
                Instantiate(prefab, randomPos, Quaternion.identity, transform);
            }
        }

    }
}
