using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpawnControl : MonoBehaviour
{
    [System.Serializable]
    public struct EnemyType
    {
        public GameObject prefab;
        public float weight;
    }

    public List<EnemyType> enemyTypes; // List of enemy types with corresponding weights
    public Transform[] spawnPoints; // Array of spawn points
    public float spawnAreaRadius = 1f; // Radius for random variation in spawn position
    public float totalWeight; // Total weight for spawning enemies
    public int totalWaves;

    public bool spawnerPause = false;




    private int numWaves = 0;

    private void Update()
    {
        if (!spawnerPause)
        {
            if (numWaves < totalWaves)
            {
                HandleWaves();
            }
            SpawnEnemiesTest();
        }
    }

    private void SpawnEnemies(float totalWeight)
    {
        numWaves++;
        while (totalWeight > 0)
        {
            // Randomly select an enemy type
            EnemyType selectedEnemyType = enemyTypes[Random.Range(0, enemyTypes.Count)];

            // Randomly select a spawn point
            Transform selectedSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Vector3 spawnPosition = selectedSpawnPoint.position + Random.insideUnitSphere * spawnAreaRadius;
            spawnPosition.y = selectedSpawnPoint.position.y; // Adjust for ground spawning, if necessary

            // Instantiate the enemy
            Instantiate(selectedEnemyType.prefab, spawnPosition, Quaternion.identity);

            // Deduct the weight of the spawned enemy from the total weight
            totalWeight -= selectedEnemyType.weight;
        }
    }
    public void SpawnEnemiesTest()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnEnemies(totalWeight);
        }
    }
    private void HandleWaves()
    {
        if (!CheckForEnemies() && readyToSpawn == true)
        {
            readyToSpawn = false;
            StartCoroutine(WaveSpawn());
        }
    }

    //Checks for remaining enemies within 1000 units
    public bool CheckForEnemies()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1000);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                readyToSpawn = true;
                return true; // An enemy is found within the radius

            }
        }
        spawnerPause = true;
        Debug.Log("SpawnerPause");
        return false; // No enemies found within the radius
    }

    //Spawns next wave after waiting for wavespawndelay
    public float waveSpawnDelay;
    public bool readyToSpawn = true;
    IEnumerator WaveSpawn()
    {
        spawnerPause = true;
        yield return new WaitForSeconds(waveSpawnDelay);
        SpawnEnemies(totalWeight);
        totalWeight += totalWeight;
    }
}

