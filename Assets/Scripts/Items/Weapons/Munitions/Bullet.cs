using System.Collections;
using UnityEngine;

public class Bullet : Munition
{
    public float bulletLifetime;

    public void Start()
    {
        StartCoroutine(TimeOut());
    }
    // Rest of your bullet code, such as collision detection
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag(targetTag))
        {
            // If it is an enemy, apply damage
            Unit target = hitInfo.GetComponent<Unit>();
            if (target != null)
            {
                
              
                target.TakeDamage(bulletDamage);
            }

            Destroy(gameObject); // Destroy the bullet after it hits
        }
        if (hitInfo.gameObject.CompareTag("HardTerrain"))
        {
            Destroy(gameObject); // Destroy the bullet after it hits
        }
    }

    IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(bulletLifetime);
        Destroy(gameObject);
    }

    
}
