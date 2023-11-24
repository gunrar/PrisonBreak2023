using UnityEngine;
using System.Collections.Generic;


public class Grappler : Enemy
{
    public List<GameObject> trapObjects; // List of GameObjects to move
    public float spreadDistance = 5f; // Distance each object should move
    public float trapSpeed = 5f; // Speed at which the objects move
    public GameObject projectile1;
    public GameObject projectile2;
    public GameObject projectile3;
    public GameObject projectile4;



    private bool trapSprung = false;
    protected override void Start()
    {
        base.Start();
        // Get all LineRenderer components attached to this GameObject

    }
    protected override void AttackPlayer()
    {
        if (attackCooldown <= 0f)
        {
            MoveTraps();

            // Reset the cooldown
            attackCooldown = attackInterval;
        }

        attackCooldown -= Time.deltaTime;
    }

    private void MoveTraps()
    {
        
        trapSprung = true;
    }

    protected override void Update()
    {
        base.Update();
        if (trapSprung)
        {
            
            if ((transform.position - projectile1.transform.position).magnitude < spreadDistance)
            {
                projectile1.transform.position += Vector3.up * trapSpeed * Time.deltaTime;
                projectile2.transform.position += Vector3.down * trapSpeed * Time.deltaTime;
                projectile3.transform.position += Vector3.left * trapSpeed * Time.deltaTime;
                projectile4.transform.position += Vector3.right * trapSpeed * Time.deltaTime;

       
            }
                
        }
        
    }


    // Override other methods if needed
}
