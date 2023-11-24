using UnityEngine;

public class Rifle : WeaponController // Inherits from Weapon
{
    
    // Update is called once per frame
    public GameObject projectile; // Assign this in the editor with your bullet prefab
    public Transform tip; // Assign this in the editor to the transform of the 'tip' object
    public override void Update()
    {
        AimAtMouse();

        if (Input.GetButtonDown("Fire1")) // Fire1 is the left mouse button by default
        {
            Fire();
        }
        
    }

    protected override void AimAtMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector2 direction = (mousePosition - transform.position).normalized;
        float desiredAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        

        // Adjust for the 90 degrees offset
        desiredAngle += 270f;
 

        // Calculate the angle of the engine base relative to the world
        float engineBaseAngle = engineBase.transform.eulerAngles.z;
        float relativeAngle = Mathf.DeltaAngle(engineBaseAngle, desiredAngle);

        // Clamp the relative angle within the attack arc
        float clampedAngle = Mathf.Clamp(relativeAngle, -attackArc / 2, attackArc / 2);

        // Apply the rotation
        transform.rotation = Quaternion.Euler(0f, 0f, engineBaseAngle + clampedAngle);
    }

    protected override void Fire()
    {
        // Instantiate the bullet prefab at the firing point's position and rotation
        GameObject bulletObject = Instantiate(projectile, tip.position, tip.rotation);

        // Get the Bullet script component from the new bullet instance
        Bullet bulletScript = bulletObject.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            // Initialize the bullet's damage and speed using values from the Rifle
            bulletScript.Initialize(damage,speed);
        }
        else
        {
            Debug.LogError("Bullet prefab does not have a Bullet script attached.");
        }
    }
}
