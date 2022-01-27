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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

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

        if (rb.velocity.magnitude <= 0.25f)
        {
            DisablePhysics();
        }

        if (pickedUp == false)
        {
            distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

            if (distanceToPlayer < 0.5f && player.gameObject.GetComponent<Shooting>().carrying == null)
            {
                if (tag == "Sharp" && rb.isKinematic == true)
                {
                    RegisterPickUp();
                    player.gameObject.GetComponent<Shooting>().PickUp(transform);
                    return;
                }
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
