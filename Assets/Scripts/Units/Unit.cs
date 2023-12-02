using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Unit : MonoBehaviour
{
    public float health = 100f;
    public float moveSpeed = 5f;

    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Move(Vector2 direction)
    {
        rb.position += direction;
        //rb.velocity = direction; // Use Rigidbody2D for movement
    }

    public virtual void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // Handle death, like playing an animation, dropping loot, etc.
        PhotonNetwork.Destroy(gameObject);
        Destroy(gameObject);
    }
}
