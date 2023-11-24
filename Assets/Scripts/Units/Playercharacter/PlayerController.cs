using UnityEngine;

public class PlayerController : Unit
{
    public GameObject reticle;
    void FixedUpdate()
    {
        // Handle Movement
        HandleMovement();

        // Rotate towards the mouse
        RotateTowardsMouse();
    }

    private void HandleMovement()
    {
        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 movement = movementInput.normalized * moveSpeed * Time.fixedDeltaTime;
        Vector2 targetPosition = (Vector2)transform.position + movement;

        if (!IsBlocked(targetPosition))
        {
            Move(movement);
        }

        
        if (IsBlocked(targetPosition))
        {
            movement = Vector2.zero;
            Move(movement);
        }

    }

    private bool IsBlocked(Vector2 targetPosition)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(targetPosition, 0.1f);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("HardTerrain"))
            {
                return true; // Blocked by an object with the tag
            }
        }
        return false; // Not blocked
    }
    private void RotateTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z; // Keep the z-coordinate unchanged



        Vector2 direction = (mousePosition - transform.position).normalized;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Determine target rotation
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        // Rotate towards the target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

}
