using UnityEngine;// This stays as a ScriptableObject because it represents data, not behavior.

public class Weapon : Item
{
    
    
    // Other weapon stats...
}

// This is a MonoBehaviour which would be attached to a GameObject in your scene.
// It controls the weapon's behavior, like aiming and firing.
public class WeaponController : MonoBehaviour
{
    public float damage;
    public float speed;
    public float range;
    public float attackArc;
    public string weaponTargetTag;
    public GameObject engineBase;
    public virtual void Update()
    {
        // Behavior methods here
        //Aim();
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    protected virtual void AimAtMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector2 direction = (mousePosition - transform.position).normalized;
        float desiredAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        // Adjust for the 90 degrees offset
        //desiredAngle += 270f;


        // Calculate the angle of the engine base relative to the world
        float engineBaseAngle = engineBase.transform.eulerAngles.z;
        float relativeAngle = Mathf.DeltaAngle(engineBaseAngle, desiredAngle);

        // Clamp the relative angle within the attack arc
        float clampedAngle = Mathf.Clamp(relativeAngle, -attackArc / 2, attackArc / 2);

        // Apply the rotation
        transform.rotation = Quaternion.Euler(0f, 0f, engineBaseAngle + clampedAngle);
    }

    protected virtual void Fire()
    {
        // Implementation of firing logic using weaponData properties
    }
}
