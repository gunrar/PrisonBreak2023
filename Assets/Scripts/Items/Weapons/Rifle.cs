using UnityEngine;

public class Rifle : WeaponController // Inherits from Weapon
{
    // Update is called once per frame
    public GameObject projectile; // Assign this in the editor with your bullet prefab
    public Transform tip; // Assign this in the editor to the transform of the 'tip' object

    public override void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Fire1 is the left mouse button by default
        {
            Fire();
        }
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
