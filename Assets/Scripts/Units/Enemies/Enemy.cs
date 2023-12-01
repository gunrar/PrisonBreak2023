using UnityEngine;
using System.Linq;
using System.Collections.Generic;


public class Enemy : Unit
{
    // You can override the Unit methods if specific behavior for Enemy is needed
    public float targetingRange;
    public float damage;
    public float attackRange;
    public float attackInterval;
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public float shootingInterval = 2f;
    public float standoffRange;

    public float enemyWeight;

    public float lastShootTime;
    public Transform playerTransform;
    public float attackCooldown;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    protected override void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
    }



    public void SetWeight(float newWeight)
    {
        enemyWeight = newWeight;
        // Additional code to handle the weight, if necessary
    }
    protected virtual void Update()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        float distanceToPlayer = 0;
        if (playerTransform != null) { 
            distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        }
        if (distanceToPlayer <= targetingRange)
        {
            // Player within targeting range, move towards the player
            RotateTowardsPlayer();
            if (distanceToPlayer > attackRange)
            {
                MoveTowardsPlayer();
            }
            if (distanceToPlayer < attackRange && distanceToPlayer > standoffRange)
            {
                MoveTowardsPlayer();
                AttackPlayer();
            }
            else
            {
                // Player within attack range, attack the player
                AttackPlayer();
            }
        }
    }

    public float minimumDistanceToAvoid;
    public float detectionRadius;
    public float avoidStrength; // How strongly to avoid objects
    public List<string> avoidTags; // List of tags to avoid
    public virtual void MoveTowardsPlayer()
    {
        //Debug.Log("trying to move");
        Vector2 moveDirection = (playerTransform.position - transform.position).normalized;

        // Use Physics2D.OverlapCircleAll for 2D physics
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        //Debug.Log("Colliders Detected: " + hitColliders.Length); // Debugging line

        Vector2 avoidanceVector = Vector2.zero;

        foreach (var hitCollider in hitColliders)
        {
            if (avoidTags.Contains(hitCollider.tag) && hitCollider.transform != transform)
            {
                Vector2 awayFromCollider = transform.position - hitCollider.transform.position;
                avoidanceVector += awayFromCollider.normalized / awayFromCollider.magnitude;
            }
        }

        avoidanceVector.Normalize();
        moveDirection += avoidanceVector * avoidStrength;

        if (moveDirection != Vector2.zero)
        {
            transform.position += (Vector3)moveDirection.normalized * moveSpeed * Time.deltaTime;
            
        }
    }






    protected virtual void RotateTowardsPlayer()
    {

        // Calculate the direction from the enemy to the player
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        // Rotate towards the player
        // Calculate the angle towards the player in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90; // Subtract 90 if the enemy sprite is facing right
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        // Apply the rotation to the enemy
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, moveSpeed * Time.deltaTime);
    }

    protected virtual void AttackPlayer()
    {
        if (attackCooldown <= 0f)
        {
            // Implement the attack logic here
            ShootAtPlayer();


            // Reset the cooldown (replace 'attackInterval' with your desired cooldown duration)
            attackCooldown = attackInterval;
        }

        attackCooldown -= Time.deltaTime;
    }

    public virtual void ShootAtPlayer()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            if (Time.time > lastShootTime + shootingInterval)
            {
                lastShootTime = Time.time;
                
                // Shoot a bullet towards the player
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Vector3 shootDirection = (playerTransform.position - transform.position).normalized;
                bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;
                // Optional: Rotate bullet to face the player
                float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

                // Get the Bullet script component from the new bullet instance
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                if (bulletScript != null)
                {
                    // Initialize the bullet's damage and speed using values from the Rifle
                    bulletScript.Initialize(damage, bulletSpeed);
                }
                else
                {
                    Debug.LogError("Bullet prefab does not have a Bullet script attached.");
                }

              
            }
        }
    }
    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);
        // Additional logic for Enemy damage, if any
    }

    protected override void Die()
    {
        // Additional logic for Enemy death, if any
        base.Die();
    }

    // Enemy-specific methods can go here
}
