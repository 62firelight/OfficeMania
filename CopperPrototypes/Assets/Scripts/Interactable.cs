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

    public bool damageable = false;

    public Vector3 knownVelocity;

    // Flag to determine who the object was thrown by
    // 0 - no one
    // 1 - player
    // 2 - enemy
    public int thrownFlag = 0;

    private Transform promptObj;

    public Rigidbody2D rb;

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
        if (rb.velocity.magnitude <= 2f)
        {
            DisablePhysics();
        }

        // If the player is near a heavy object
        if (isHeavy == true && pickedUp == false)
        {
            Interactable obj = player.gameObject.GetComponent<PlayerThrowing>().nearestObject;
            
            if (this == obj && player.gameObject.GetComponent<PlayerThrowing>().bluntObject == null)
            {
                // Display prompt if player is near
                promptObj.gameObject.SetActive(true);
            }
            else
            {
                // Hide prompt if object has been picked up or player is not near
                promptObj.gameObject.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        knownVelocity = rb.velocity;

        if (knownVelocity.magnitude <= 3f)
        {
            damageable = false;
        }
    }

    public void Throw(Transform objectPoint, float force)
    {
        // Alter bounciness depending on charge 
        if (coll.sharedMaterial != null)
        {
            float percent = (force - 20f) / 40f * 100;

            if (percent > 25)
            {
                coll.sharedMaterial.bounciness = 0.9f;
            }
        }

        Debug.Log("Object Bounciness: " + coll.bounciness);

        thrownFlag = 1;
        damageable = true;

        EnablePhysics();
        pickedUp = false;

        rb.AddForce(objectPoint.up * force, ForceMode2D.Impulse);
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), coll);
        
        transform.Translate(0, 0, 1);
        transform.parent = null;

        AIMaster.takenObjects.Remove(gameObject);
    }

    public void EnemyThrow(Transform objectPoint, float force, GameObject enemy)
    {
        thrownFlag = 2;
        damageable = true;

        EnablePhysics();
        pickedUp = false;

        Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), coll);
        if (isHeavy)
        {
            Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), transform.GetChild(1).GetComponent<Collider2D>());
        }

        rb.AddForce(objectPoint.up * force, ForceMode2D.Impulse);
        
        transform.Translate(0, 0, 1);
        transform.parent = null;

        AIMaster.takenObjects.Remove(gameObject);
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
            coll.sharedMaterial.bounciness = 0.9f;
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
        // if (tag == "Blunt" && player.gameObject.GetComponent<PlayerThrowing>().bluntObject != null)
        // {
        //     return;
        // }

        DisablePhysics();
        pickedUp = true;
        promptObj.gameObject.SetActive(false);
        player.gameObject.GetComponent<PlayerThrowing>().PickUp(transform);
        AIMaster.takenObjects.Add(gameObject);
    }

    public void RegisterEnemyPickUp()
    {
        DisablePhysics();
        pickedUp = true;
        promptObj.gameObject.SetActive(false);
        AIMaster.takenObjects.Add(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            DisablePhysics();
        }
    }
}
