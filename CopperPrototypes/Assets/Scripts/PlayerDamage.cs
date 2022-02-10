using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{

    public int playerMaxHealth = 3;

    public int playerHealth;

    public float invulnerableCooldown = 0f;

    private Rigidbody2D rb;

    void Start()
    {
        // Set player health
        playerHealth = playerMaxHealth;

        rb = GetComponent<Rigidbody2D>();

        Physics2D.IgnoreLayerCollision(6, 8, false);
    }
    
    void Update()
    {
        if (invulnerableCooldown > 0)
        {
            invulnerableCooldown -= Time.deltaTime;

            // Flash the player sprite to indicate invulnerable cooldown is still active
            if (invulnerableCooldown > 0)
            {
                GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
            }
            else
            {
                GetComponent<Renderer>().enabled = true;
                Physics2D.IgnoreLayerCollision(6, 8, false);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Don't register damage if player is invulnerable
        if (invulnerableCooldown > 0)
        {
            return;
        }

        // Check if the player collided with an enemy
        if (other.gameObject.tag == "Enemy")
        {
            RegisterDamage(other);
        }

        // Check if the player collided with a thrown object
        Interactable obj = other.gameObject.GetComponent<Interactable>();
        if (obj == null)
        {
            return;
        }

        if (obj.knownVelocity.magnitude < 2.5f)
        {
            return;
        }

        // If the player has collided, and the object was thrown by an enemy,
        // let the player take damage
        if (obj != null && obj.thrownFlag == 2)
        {
            Debug.Log(obj + " had " + obj.rb.velocity.magnitude + " velocity");
            RegisterDamage(other);
        }
    }

    void RegisterDamage(Collision2D other)
    {
        gameObject.GetComponent<BarthaSzabolcs.Tutorial_SpriteFlash.ColoredFlash>().Flash(Color.white);

        // Decrement health
        Debug.Log("Player damaged");
        playerHealth--;

        if (playerHealth <= 0)
        {
            Debug.Log("Player dead");
            SceneManager.LoadScene("GameOver");
        }
        
        invulnerableCooldown = 1.5f;
        Physics2D.IgnoreLayerCollision(6, 8, true);

        // Knock the player back
        // Vector2 diff = (transform.position - other.gameObject.transform.position).normalized;
        // Vector2 force = diff * 5f;
        // GetComponent<PlayerMovement>().knockback = true;
        // GetComponent<PlayerMovement>().knockbackForce = force;
        // rb.AddForce(force, ForceMode2D.Impulse);
    }
}
