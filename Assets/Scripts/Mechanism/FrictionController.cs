using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrictionController : MonoBehaviour
{

    public GameObject[] playerCharacters;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        // Apply friction if not moving
        //if (!isMoving)
        //{
        //    ApplyFriction1();
        //}
        //else
        //{
        //    ApplyFriction2();
        //}
    }

    //Friction When not moving
    //private void ApplyFriction1()
    //{
    //    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    //    Vector2 frictionForce = -rb.velocity * frictionCoefficient;
    //    rb.AddForce(frictionForce);
    //    otherPlayer.GetComponent<Rigidbody2D>().AddForce(-frictionForce);
    //}

    ////Friction when moving
    //public float friction2Coefficient;
    //private void ApplyFriction2()
    //{
    //    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    //    Vector2 frictionForce = -rb.velocity * friction2Coefficient;
    //    rb.AddForce(frictionForce);
    //    otherPlayer.GetComponent<Rigidbody2D>().AddForce(-frictionForce);
    //}
}
