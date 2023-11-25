using System.Collections;
using UnityEngine;

public class PlayerController : Unit
{
    
    public float distanceThreshold;

    public GameObject otherPlayer;
    private bool distanceExceeded = false;
    void FixedUpdate()
    {
        LimitMovement();
        HandleStamina();
        HandleMovement();
                
    }
    protected override void Start()
    {
        base.Start();
        moveSpeedStart = moveSpeed;
    }
    public float frictionCoefficient;
    private void HandleMovement()
    {
        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        bool isMoving = movementInput.magnitude > 0.01f; // Check if there's significant movement input
        Vector2 movement = movementInput.normalized * moveSpeed * Time.fixedDeltaTime;
        Vector2 targetPosition = (Vector2)transform.position + movement;
        Vector3 targetPositionV3 = new Vector3(targetPosition.x, targetPosition.y, 0);

        if ((targetPositionV3 - otherPlayer.transform.position).magnitude < distanceThreshold && distanceExceeded == true)
        {
            distanceExceeded = false;
        }

        if (!distanceExceeded)
        {
            if (!IsBlocked(targetPosition))
            {
                Move(movement);
            }
            else
            {
                movement = Vector2.zero;
                Move(movement);
            }
        }
        else
        {
            Debug.Log("Distance Exceeded");
            stamina = stamina - staminaUsageCoefficient * Time.deltaTime;
            if (!IsBlocked(targetPosition))
            {
                Move(movement);
                TetherEffect();
            }
            else
            {
                movement = Vector2.zero;
                Move(movement);
                TetherEffect();
            }
        }

        // Apply friction if not moving
        if (!isMoving)
        {
            ApplyFriction1();
        }
        else
        {
            ApplyFriction2();
        }
    }


    
    //Friction When not moving
    private void ApplyFriction1()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 frictionForce = -rb.velocity * frictionCoefficient;
        rb.AddForce(frictionForce);
        otherPlayer.GetComponent<Rigidbody2D>().AddForce(-frictionForce);
    }

    //Friction when moving
    public float friction2Coefficient;
    private void ApplyFriction2()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 frictionForce = -rb.velocity * friction2Coefficient;
        rb.AddForce(frictionForce);
        otherPlayer.GetComponent<Rigidbody2D>().AddForce(-frictionForce);
    }

    public float avoidDist;
    private bool IsBlocked(Vector2 targetPosition)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(targetPosition, avoidDist);
        foreach (var collider in colliders)
        {
            // Check if the collider is not the current object's collider and has the "Player" tag
            if (collider.gameObject != gameObject && collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("isBlocked");
                return true; // Blocked by an object with the tag
            }
        }
        return false; // Not blocked
    }

    //Checks if the distance between the two players is greater than distance threshold
    public void LimitMovement()
    {

        if ((transform.position - otherPlayer.transform.position).magnitude > distanceThreshold)
        {
            distanceExceeded = true;
        }
        else
        {
            distanceExceeded = false;
        }
        
    }

    public float springConstant = 1.0f; // Adjust this to change the "strength" of the spring

    public float tetherForceCap;
    public void TetherEffect()
    {
        if (otherPlayer != null)
        {
            Vector2 direction = otherPlayer.transform.position - transform.position;
            float distance = direction.magnitude;
            Vector2 force = direction.normalized * ((distance - distanceThreshold) * (distance - distanceThreshold) * springConstant);

            float forceMag = force.magnitude;
            if (forceMag <= tetherForceCap)
            {
                rb.AddForce(force);
                otherPlayer.GetComponent<Rigidbody2D>().AddForce(-force);
            }
            
        }
    }

    public float stamina;
    public float exhaustDrainModifier;
    public float moveSpeedStart;
    public float exhaustRecoveryTime;
    public float staminaUsageCoefficient;

    private bool exhausted = false;
    private bool regainingStamina = false;
    public void HandleStamina()
    {
        Debug.Log(regainingStamina);
        if (!regainingStamina)
        {
            
            if (stamina <= 0)
            {
                exhausted = true;
                moveSpeed = moveSpeedStart / exhaustDrainModifier;
                regainingStamina = true;
            }
        }
        if (regainingStamina)
        {
            StartCoroutine(RegainStamina());
        }

    }

    IEnumerator RegainStamina()
    {
        Debug.Log("RegainingStamina");
        yield return new WaitForSeconds(exhaustRecoveryTime);
        exhausted = false;
        moveSpeed = moveSpeedStart;
        regainingStamina = false;
        stamina = 100;


    }


}

    



