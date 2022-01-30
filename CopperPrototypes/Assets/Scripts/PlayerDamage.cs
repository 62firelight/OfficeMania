using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{

    public int playerMaxHealth = 3;

    public int playerHealth;

    private Rigidbody2D rb;

    void Start()
    {
        // Set player health
        playerHealth = playerMaxHealth;

        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Check if the player collided with an enemy
        if (other.gameObject.tag == "Enemy")
        {
            RegisterDamage(other);
        }

        // Check if the player collided with a thrown object
        Interactable obj = other.gameObject.GetComponent<Interactable>();

        // If the player has collided, and the object was thrown by an enemy,
        // let the player take damage
        if (obj != null && obj.thrownFlag == 2)
        {
            RegisterDamage(other);
        }
    }

    void RegisterDamage(Collision2D other)
    {
        // Decrement health
        Debug.Log("Player damaged");
        playerHealth--;

        if (playerHealth <= 0)
        {
            Debug.Log("Player dead");
        }

        // Knock the player back
        // Vector2 diff = (transform.position - other.gameObject.transform.position).normalized;
        // Vector2 force = diff * 5f;
        // GetComponent<PlayerMovement>().knockback = true;
        // GetComponent<PlayerMovement>().knockbackForce = force;
        // rb.AddForce(force, ForceMode2D.Impulse);
    }
}
