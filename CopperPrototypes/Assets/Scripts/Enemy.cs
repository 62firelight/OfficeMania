using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 1;

    public int health;

    public float knockbackForce = 50f;

    public float knockbackTime = 0f;

    public bool isBoss = false;

    public int bossHealth = 3;

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

        if (isBoss == false)
        {
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
        else
        {
            if (health <= 0 && bossHealth > 0)
            {
                bossHealth--;
                health = maxHealth;

                DynamicMusic music = GetComponent<DynamicMusic>();

                if (music == null)
                {
                    Debug.Log("Could not find DynamicMusic component for " + gameObject.name + "!");
                    Time.timeScale = 0;
                }

                music.TriggerFadeOut();

                if (bossHealth <= 0)
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
            if (!(GetComponent<Chase>() == null) && SceneManager.GetActiveScene().name != "Level2")
            {
                GetComponent<Chase>().roomMaster.GetComponent<RoomMaster>().seePlayer = true;
            }
        }

        Interactable obj = other.gameObject.GetComponent<Interactable>();
        if (obj == null)
        {
            return;
        }

        if (obj.knownVelocity.magnitude < 1.5f)
        {
            Debug.Log(other.gameObject.GetComponent<Interactable>().knownVelocity.magnitude);
            return;
        }

        if (other.gameObject.tag == "Blunt" || other.gameObject.tag == "Sharp")
        {
            Debug.Log(gameObject.name + " flag: " + obj.thrownFlag);
            // Deduct health if thrown by player
            if (obj.thrownFlag < 2)
            {
                gameObject.GetComponent<BarthaSzabolcs.Tutorial_SpriteFlash.ColoredFlash>().Flash(Color.white);
                health--;
            }
            else
            {
                // Deduct health randomly if thrown by another enemy
                int roll = Random.Range(1, 6);

                if (roll == 1)
                {
                    gameObject.GetComponent<BarthaSzabolcs.Tutorial_SpriteFlash.ColoredFlash>().Flash(Color.white);
                    health--;
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

                if (health <= 0)
                {
                    GetComponentInChildren<Dialogue>().DisplayDialogue(DialogueMaster.GetEnemyKOLine());
                }
            }
            else if (other.gameObject.tag == "Sharp")
            {
                obj.DisablePhysics();
                other.gameObject.transform.parent = transform;
                obj.pickedUp = true;

                if (GetComponent<Chase>() != null)
                {
                    if (isBoss == false)
                    {
                        GetComponent<Chase>().speed *= 0.75f;
                    }
                    else
                    {
                        GetComponent<Chase>().speed *= 0.9f;
                    }
                }

                if (health <= 0)
                {
                    GetComponentInChildren<Dialogue>().DisplayDialogue(DialogueMaster.GetEnemyStuckLine());
                }
            }
        }

        if (health > 0)
        {
            if (other.gameObject.tag == "Blunt" || other.gameObject.tag == "Sharp")
            {
                if (GetComponent<Chase>() != null && GetComponent<Chase>().seePlayer == false)
                {
                    GetComponent<Chase>().roomMaster.GetComponent<RoomMaster>().SetSeePlayer(true);
                }
            }
        }

        
    }
}
