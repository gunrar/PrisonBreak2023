using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Unit
{
    [System.Serializable]
    public class EnemySpawn
    {
        public GameObject enemyPrefab;
        public int enemyWeight;
    }

    public List<EnemySpawn> enemyPrefabs;
    public int spawnWeight;
    public float spawnRange; // The radius within which enemies can be spawned

    protected override void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        int currentWeight = 0;
        Debug.Log("First Spawn");
        while (currentWeight < spawnWeight)
        {
            Debug.Log("2");
            EnemySpawn chosenEnemySpawn = ChooseRandomEnemyByWeight();
            if (currentWeight + chosenEnemySpawn.enemyWeight <= spawnWeight)
            {
                Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRange;
                spawnPosition.y = transform.position.y; // Assuming you're working in 3D and want to keep the y-coordinate the same
                Debug.Log(chosenEnemySpawn);
                GameObject enemy = Instantiate(chosenEnemySpawn.enemyPrefab, spawnPosition, Quaternion.identity);
                currentWeight += chosenEnemySpawn.enemyWeight;
                Debug.Log("3");
            }
            else
            {
                // Break the loop if adding another enemy would exceed the spawnWeight
                Debug.Log("4");
                break;
            }
        }
    }

    EnemySpawn ChooseRandomEnemyByWeight()
    {
        int totalWeight = 0;
        foreach (EnemySpawn es in enemyPrefabs)
        {
            totalWeight += es.enemyWeight;
        }

        int randomWeight = Random.Range(0, totalWeight);
        int currentWeight = 0;

        foreach (EnemySpawn es in enemyPrefabs)
        {
            currentWeight += es.enemyWeight;
            if (currentWeight >= randomWeight)
            {
                return es;
            }
        }

        // Fallback, should not normally reach here
        return enemyPrefabs[0];
    }
}
