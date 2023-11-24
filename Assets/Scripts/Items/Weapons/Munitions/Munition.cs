using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Munition : MonoBehaviour
{
    // Start is called before the first frame update
    public float bulletDamage;
    public float bulletSpeed;
    public string targetTag;


    public void Initialize(float damage, float speed)
    {
        bulletDamage = damage;
        bulletSpeed = speed;

        // Assuming the bullet moves along the local up vector.
        GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
