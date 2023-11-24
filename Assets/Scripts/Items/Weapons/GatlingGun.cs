using UnityEngine;

public class GatlingGun : WeaponController
{
    public GameObject RotationPoint;
    public float maxRotationAngle = 45f;
    public float armRotationSpeed = 5f; // Speed at which the arm rotates
    public float RotationOffset; // Rotation offset in degrees

    public override void Update()
    {
        AimAtMouse();
        if (Input.GetButtonDown("Fire1")) // Fire1 is the left mouse button by default
        {
            Fire();
        }
    }


    //PARTIALLY DOESNT WORK CURRENTLY ROTATES IN 360

    protected override void AimAtMouse() 
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        //Debug.Log("Mouse position"+ mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        float desiredAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Adjust for the 90 degrees offset
        desiredAngle += 270f;
        //Debug.Log("desired angle" + mousePosition);
        // Calculate the angle of the engine base relative to the world
        float engineBaseAngle = RotationPoint.transform.eulerAngles.z;
        float relativeAngle = Mathf.DeltaAngle(engineBaseAngle, desiredAngle);

        // Clamp the relative angle within the attack arc
        float clampedAngle = Mathf.Clamp(relativeAngle, -attackArc / 2, attackArc / 2);
        Debug.Log("clamped angle" + clampedAngle);
        // Determine the target rotation
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, engineBaseAngle + clampedAngle);
        Debug.Log("targetRotation" + targetRotation);
        // Smoothly rotate towards the target rotation
        RotationPoint.transform.rotation = Quaternion.RotateTowards(RotationPoint.transform.rotation, targetRotation, armRotationSpeed * Time.deltaTime);
    }
}

