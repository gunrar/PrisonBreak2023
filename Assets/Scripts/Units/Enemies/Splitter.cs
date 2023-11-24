using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Splitter : Enemy
{
    public float spreadAngle = 5f; // The angle of spread for the projectiles
    public GameObject spawnPrefab1; // The prefab for the GameObject to spawn
    public GameObject spawnPrefab2; // The prefab for the GameObject to spawn
    public float spawnDist = 2f; // Distance between the spawned GameObjects

    public override void ShootAtPlayer()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            if (Time.time > lastShootTime + shootingInterval)
            {
                lastShootTime = Time.time;
                
                // Fire three projectiles in an arc
                for (int i = -1; i <= 1; i++)
                {
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    Vector3 shootDirection = (playerTransform.position - transform.position).normalized;

                    // Rotate the shoot direction by the spread angle
                    shootDirection = Quaternion.Euler(0, 0, spreadAngle * i) * shootDirection;

                    bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;

                    // Rotate bullet to face the correct direction
                    float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
                    bullet.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

                    Bullet bulletScript = bullet.GetComponent<Bullet>();
                    if (bulletScript != null)
                    {
                        bulletScript.Initialize(damage, bulletSpeed);
                    }
                    else
                    {
                        Debug.LogError("Bullet prefab does not have a Bullet script attached.");
                    }
                }
            }
        }
    }

    protected override void Die()
    {
        SpawnObjects();
        Destroy(gameObject);
    }

    public void SpawnObjects()
    {
        // Calculate the positions for the two GameObjects
        Vector3 spawnPos1 = transform.position + transform.right * spawnDist / 2;
        Vector3 spawnPos2 = transform.position - transform.right * spawnDist / 2;

        // Instantiate the GameObjects at the calculated positions
        Instantiate(spawnPrefab1, spawnPos1, Quaternion.identity);
        Instantiate(spawnPrefab2, spawnPos2, Quaternion.identity);
    }
}

