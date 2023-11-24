using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : Enemy
{
    // Start is called before the first frame update

    public Transform target;
    public float maxSpinSpeed = 3000f; // Maximum spin speed in degrees per second
    public float SDRange;
    protected override void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        playerTransform = target;
    }

    // Update is called once per frame
    protected override void Update()
    {
        SpinAtTarget();
        MoveTowardsPlayer();
    }

    public void SpinAtTarget()
    {
        
        if (target != null)
        {
            // Calculate distance to the target
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            // Calculate spin speed based on distance (closer = faster spin)
            float spinSpeed = Mathf.Lerp(maxSpinSpeed, 0, distanceToTarget / 50.0f);
            spinSpeed = Mathf.Clamp(spinSpeed, 0, maxSpinSpeed);
            // Rotate the GameObject around the Z-axis
            transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);

            if (distanceToTarget < SDRange)
            {
                Die();
            }
        }
    }
    protected override void Die()
    {
        FireStarburst();
        Destroy(gameObject);
    }


    public int numDeathProj = 5; // Number of projectiles in the starburst
    public float deathBulletDamage = 10f;
    public float deathBulletSpeed = 5f;
    public float fireVariance;
    public void FireStarburst()
    {
        // Calculate the angle step for each bullet based on the total number
        float angleStep = 360f / numDeathProj;

        for (int i = 0; i < numDeathProj; i++)
        {
            // Calculate the base rotation for each bullet
            float baseAngle = i * angleStep;

            // Apply a random variance to the angle
            float variance = Random.Range(-fireVariance, fireVariance);
            float variedAngle = baseAngle + variance;
            Quaternion variedRotation = Quaternion.Euler(0, 0, variedAngle);

            // Instantiate the bullet with the varied rotation
            GameObject bullet = Instantiate(bulletPrefab, transform.position, variedRotation);

            // Initialize bullet properties
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.Initialize(deathBulletDamage, deathBulletSpeed);
            }
        }
    }
}
