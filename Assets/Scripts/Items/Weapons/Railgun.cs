using UnityEngine;

public class Railgun : WeaponController
{
    public GameObject projectile;
    public Transform tip;
    public float fireDelay = 1f; // Time in seconds between shots
    private float lastFireTime = 0f; // Timer to track delay between shots

    public override void Update()
    {

        if (Input.GetButtonDown("Fire1")) // Fire1 is the left mouse button by default
        {
            Fire();
        }
        lastFireTime += Time.deltaTime;
    }

    protected override void Fire()
    {
        // Check if enough time has passed since the last shot
        if (lastFireTime >= fireDelay)
        {
            // Instantiate the bullet prefab at the firing point's position and rotation
            GameObject bulletObject = Instantiate(projectile, tip.position, tip.rotation);

            // Get the Bullet script component from the new bullet instance
            Munition bulletScript = bulletObject.GetComponent<Munition>();
            if (bulletScript != null)
            {
                // Initialize the bullet's damage and speed using values from the Rifle
                bulletScript.Initialize(damage, speed);
                Debug.Log("BLASTIN");
            }
            else
            {
                Debug.LogError("Bullet prefab does not have a Bullet script attached.");
            }

            // Reset the fire timer
            lastFireTime = 0f;
        }
    }
}

