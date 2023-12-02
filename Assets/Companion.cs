using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : Unit
{
    public GameObject playerObject;

    public void Update()
    {
        GetComponent<Companion>().playerObject.GetComponent<PlayerControllerNetworked>().otherPlayer = transform.gameObject;
    }
    public override void TakeDamage(float amount)
    {
        playerObject.GetComponent<PlayerControllerNetworked>().health -= amount * 2;
        if (health <= 0)
        {
            Die();
        }
    }
}
