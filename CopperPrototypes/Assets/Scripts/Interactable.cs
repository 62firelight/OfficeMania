using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public Transform prompt;

    public GameObject player;

    public float distanceToPlayer;

    public bool isHeavy = false;

    public bool pickedUp = false;

    // Flag to determine who the object was thrown by
    // 0 - no one
    // 1 - player
    // 2 - enemy
    public int thrownFlag = 0;

    private Transform promptObj;

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
            // For a sharp object (that is still), let the player pick it up if they are near
            if (tag == "Sharp" && rb.isKinematic == true)
            {
                bool playerNear = GetComponentInChildren<SharpTrigger>().playerNear;

                if (playerNear)
                {
                    RegisterPickUp();
                }
            }
            // For a blunt object, just show a prompt
            else
            {
                Interactable obj = player.gameObject.GetComponent<PlayerThrowing>().nearestObject;

                // If the player is near and they're not carrying anything
                if (this == obj && player.gameObject.GetComponent<PlayerThrowing>().bluntObject == null)
                {
                    promptObj.gameObject.SetActive(true);
                }
                else
                {
                    // Hide prompt if object has been picked up
                    promptObj.gameObject.SetActive(false);
                }
            }
        }
    }
    
    public void Throw(Transform firePoint, float force)
    {
        // Alter bounciness depending on charge 
        if (coll.sharedMaterial != null)
        {
            float percent = (force - 20f) / 40f * 100;

            if (percent > 25)
            {
                coll.sharedMaterial.bounciness = 0.5f;
            }
        }

        Debug.Log("Object Bounciness: " + coll.bounciness);

        EnablePhysics();
        pickedUp = false;

        rb.AddForce(firePoint.up * force, ForceMode2D.Impulse);
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), coll);
        
        transform.Translate(0, 0, 1);
        transform.parent = null;

        thrownFlag = 1;
    }

    public void EnemyThrow(Transform firePoint, float force, GameObject enemy)
    {
        EnablePhysics();
        pickedUp = false;

        Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), coll);
        if (isHeavy)
        {
            Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), transform.GetChild(1).GetComponent<Collider2D>());
        }

        rb.AddForce(firePoint.up * force, ForceMode2D.Impulse);
        
        transform.Translate(0, 0, 1);
        transform.parent = null;

        thrownFlag = 2;
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

        if (coll.sharedMaterial != null)
        {
            coll.sharedMaterial.bounciness = 0.5f;
        }
        thrownFlag = 0;
    }

    void CreatePrompt()
    {
        promptObj = Instantiate(prompt, transform.position + new Vector3(0, 0.65f, -9), Quaternion.identity);
        promptObj.parent = transform;
        promptObj.gameObject.SetActive(false);
    }

    public void RegisterPickUp()
    {
        if (tag == "Blunt" && player.gameObject.GetComponent<PlayerThrowing>().bluntObject != null)
        {
            return;
        }

        DisablePhysics();
        pickedUp = true;
        promptObj.gameObject.SetActive(false);
        player.gameObject.GetComponent<PlayerThrowing>().PickUp(transform);
    }

    public void RegisterEnemyPickUp()
    {
        DisablePhysics();
        pickedUp = true;
        promptObj.gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            DisablePhysics();
        }
    }
}
