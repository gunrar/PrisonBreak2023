using UnityEngine;
using System.Collections.Generic;

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

    private void Start()
    {
        SpawnEnemies(totalWeight);
    }

    private void SpawnEnemies(float totalWeight)
    {
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
}

