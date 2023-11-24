using UnityEngine;

public class Brawler : Enemy
{
    public float dashSpeed = 20f; // Speed of the dash
    public float dashDuration = 0.5f; // Duration of the dash
    public float dashCooldown = 2f; // Cooldown between dashes


    private float dashEndTime;
    private float lastDashTime;
    private bool hasHit = false;

    
    private bool isDashing;
    private Vector2 dashDirection;


    protected override void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on " + gameObject.name);
        }
        // Assuming player object is tagged as "Player"
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player object not found!");
        }

        //For Brawler's moveDirection()
        moveDirection = (playerTransform.position - transform.position).normalized;
    }
    protected override void Update()
    {
        base.Update(); // Call the base class update

        if (isDashing)
        {
            if (Time.time > dashEndTime)
            {
                EndDash();
            }
        }
        else
        {
            
            
            if (Time.time > lastDashTime + dashCooldown)
            {
                RotateTowardsPlayer();

                if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
                {
                    StartDash();
                }
                
            }
        }
        if (Time.time > dashEndTime)
        {
            hasHit = false;
        }
    }

    private void StartDash()
    {

        isDashing = true;
        moveSpeed = moveSpeed * dashSpeed; // Set the Rigidbody's velocity in the dash direction
        dashEndTime = Time.time + dashDuration;
        lastDashTime = Time.time;
    }

    private void EndDash()
    {
        isDashing = false;
        moveSpeed = moveSpeed / dashSpeed;
        hasHit = false;

    }


    public string targetTag;
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag(targetTag) && !hasHit)
        {
            // If it is an enemy, apply damage
            Unit target = hitInfo.GetComponent<Unit>();
            if (target != null)
            {

                target.TakeDamage(damage);
                hasHit = true;
            }

        }


    }

    // Override the AttackPlayer method to prevent shooting
    protected override void AttackPlayer()
    {
        //StartDash();
    }

    protected override void RotateTowardsPlayer()
    {
        if (!isDashing)
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
    }

    private Vector2 moveDirection;
    public override void MoveTowardsPlayer()
    {


        //Debug.Log("trying to move");
        if (!isDashing)
        {
            moveDirection = (playerTransform.position - transform.position).normalized;
        }
        // Use Physics2D.OverlapCircleAll for 2D physics
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        //Debug.Log("Colliders Detected: " + hitColliders.Length); // Debugging line

        Vector2 avoidanceVector = Vector2.zero;
        if (moveDirection != null)
        {
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
    }
}
