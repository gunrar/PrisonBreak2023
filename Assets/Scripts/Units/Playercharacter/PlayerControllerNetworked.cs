using System.Collections;
using UnityEngine;
using Photon.Pun;

public class PlayerControllerNetworked : Unit
{

    PhotonView view;

    public float stamina = 100f;
    public float exhaustDrainModifier = 3f;
    public float moveSpeedStart = 0f;
    public float exhaustRecoveryTime = 3f;
    public float staminaUsageCoefficient = 10f;
    public float frictionCoefficient = 10f;
    public float friction2Coefficient = 1f;
    public float avoidDist = 0.5f;
    public float rotationSpeed = 10f;


    void FixedUpdate()
    {
        if (view.IsMine)
        {
            //HandleStamina();
            HandleMovement();
            RotateTowardsMouse();
        }

    }
    protected override void Start()
    {
        base.Start();
        moveSpeedStart = moveSpeed;
        view = GetComponent<PhotonView>();
    }
    private void HandleMovement()
    {
        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        bool isMoving = movementInput.magnitude > 0.01f; // Check if there's significant movement input
        Vector2 movement = movementInput.normalized * moveSpeed * Time.fixedDeltaTime;
        Vector2 targetPosition = (Vector2)transform.position + movement;
        Vector3 targetPositionV3 = new Vector3(targetPosition.x, targetPosition.y, 0);
        if (!IsBlocked(targetPosition))
        {
            Move(movement);
        }
        else
        {
            movement = Vector2.zero;
            Move(movement);
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

    private void RotateTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z; // Keep the z-coordinate unchanged

        Vector2 direction = (mousePosition - transform.position).normalized;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Determine target rotation
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Rotate towards the target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    //Friction When not moving
    private void ApplyFriction1()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 frictionForce = -rb.velocity * frictionCoefficient;
        rb.AddForce(frictionForce);
    }

    //Friction when moving
    private void ApplyFriction2()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 frictionForce = -rb.velocity * friction2Coefficient;
        rb.AddForce(frictionForce);
    }

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

    private bool exhausted = false;
    private bool regainingStamina = false;
    //public void HandleStamina()
    //{
    //    if (!regainingStamina)
    //    {

    //        if (stamina <= 0)
    //        {
    //            exhausted = true;
    //            moveSpeed = moveSpeedStart / exhaustDrainModifier;
    //            regainingStamina = true;
    //        }
    //    }
    //    if (regainingStamina)
    //    {
    //        StartCoroutine(RegainStamina());
    //    }

    //}

    //IEnumerator RegainStamina()
    //{
    //    Debug.Log("RegainingStamina");
    //    yield return new WaitForSeconds(exhaustRecoveryTime);
    //    exhausted = false;
    //    moveSpeed = moveSpeedStart;
    //    regainingStamina = false;
    //    stamina = 100;


    //}


}





