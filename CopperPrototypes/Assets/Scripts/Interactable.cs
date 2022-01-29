using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public Transform prompt;

    public GameObject player;

    public float distanceToPlayer;

    private Transform promptObj;

    public bool pickedUp = false;

    private Rigidbody2D rb;

    private Collider2D coll;

    private InteractTrigger interactTrigger;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        interactTrigger = GetComponentInChildren<InteractTrigger>();

        DisablePhysics();
        CreatePrompt();
    }
    
    public void Throw(Transform firePoint, float force)
    {
        EnablePhysics();

        pickedUp = false;

        rb.AddForce(firePoint.up * force, ForceMode2D.Impulse);
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), coll);
        
        transform.Translate(0, 0, 1);
        transform.parent = null;
    }

    public void EnemyThrow(Transform firePoint, float force, GameObject enemy)
    {
        EnablePhysics();

        pickedUp = false;

        rb.AddForce(firePoint.up * force, ForceMode2D.Impulse);
        Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), coll);

        transform.Translate(0, 0, 1);
        transform.parent = null;
    }

    public void EnablePhysics()
    {
        rb.isKinematic = false;
        coll.enabled = true;
    }

    public void DisablePhysics()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;

        rb.isKinematic = true;
        coll.enabled = false;
    }

    void CreatePrompt()
    {
        promptObj = Instantiate(prompt, transform.position + new Vector3(0, 0.5f, -9), Quaternion.identity);
        promptObj.parent = transform;
        promptObj.gameObject.SetActive(false);
    }

    public void RegisterPickUp()
    {
        DisablePhysics();

        pickedUp = true;
        promptObj.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Disable physics if the object is moving at a slow enough speed
        if (rb.velocity.magnitude <= 0.25f)
        {
            DisablePhysics();
        }

        if (pickedUp == false)
        {
            bool playerNear = interactTrigger.playerNear;

            // If the player is near and they're not carrying anything
            if (playerNear && player.gameObject.GetComponent<Shooting>().carrying == null)
            {
                // For a sharp object, let the player pick it up
                if (tag == "Sharp" && rb.isKinematic == true)
                {
                    RegisterPickUp();
                    player.gameObject.GetComponent<Shooting>().PickUp(transform);
                    return;
                }
                // For a blunt object, show a prompt and let the player pick it up only they push a button
                else if (tag == "Blunt")
                {
                    promptObj.gameObject.SetActive(true);

                    if (Input.GetButtonDown("Fire2"))
                    {
                        RegisterPickUp();
                        player.gameObject.GetComponent<Shooting>().PickUp(transform);
                    }
                }
            }
            else
            {
                promptObj.gameObject.SetActive(false);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            DisablePhysics();
        }
    }
}
