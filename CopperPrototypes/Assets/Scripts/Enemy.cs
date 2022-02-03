using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 1;

    public int health;

    public float knockbackForce = 5f;

    public float knockbackTime = 0f;

    // Rigidbody2D component
    private Rigidbody2D rb;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        health = maxHealth;

        DisableRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        // Decrement knockback time
        if (knockbackTime > 0)
        {
            knockbackTime -= Time.deltaTime;

            // If knockback time is exceeded, set velocity to 0 and disable physics
            if (knockbackTime <= 0)
            {
                rb.velocity = Vector2.zero;
                DisableRagdoll();
            }
        }

        // If enemy has no health
        if (health <= 0)
        {
            // Darken sprite
            sr.color = new Color(0.5f, 0, 0);

            // Disable AI
            if (GetComponent<Chase>() != null)
            {
                GetComponent<Chase>().speed = 0;
            }

            rb.simulated = false;
        }
    }

    // Let the rigidbody take control and detect collisions.
    void EnableRagdoll()
    {
        rb.isKinematic = false;
    }

    // Let animation control the rigidbody and ignore collisions.
    void DisableRagdoll()
    {
        rb.isKinematic = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Blunt" || other.gameObject.tag == "Sharp")
        {
            if (GetComponent<Chase>() == null)
            {
                
            }
            else
            {
                GetComponent<Chase>().roomMaster.GetComponent<RoomMaster>().seePlayer = true;
            }
            
        }
        
        if (other.gameObject.tag == "Blunt")
        {
            // Do nothing if we are currently being knocked back
            if (knockbackTime > 0)
            {
                return;
            }

            Rigidbody2D otherRb = other.gameObject.GetComponent<Rigidbody2D>();

            Vector2 diff = (transform.position - other.gameObject.transform.position).normalized;
            Vector2 force = diff * knockbackForce;

            EnableRagdoll();
            rb.AddForce(force, ForceMode2D.Impulse);

            knockbackTime = 0.4f;

            health--;
        }
        else if (other.gameObject.tag == "Sharp")
        {
            health--;

            if (GetComponent<Chase>() != null)
            {
                GetComponent<Chase>().speed *= 0.75f;
            }

            other.gameObject.GetComponent<Interactable>().DisablePhysics();
            other.gameObject.transform.parent = transform;
            other.gameObject.GetComponent<Interactable>().pickedUp = true;
        }

        gameObject.GetComponent<BarthaSzabolcs.Tutorial_SpriteFlash.ColoredFlash>().Flash(Color.white);
    }
}
