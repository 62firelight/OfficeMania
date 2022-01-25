using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    public Vector2 movement;

    public int maxHealth = 1;

    private int health;

    private Rigidbody2D rb;

    private SpriteRenderer sr;

    private bool isColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        health = maxHealth;

        if (movement == Vector2.zero)
        {
            movement = new Vector2(2f, 0);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (health <= 0)
        {
            sr.color = new Color(0.5f, 0, 0);
            movement = Vector2.zero;
        }

        isColliding = false;
        rb.MovePosition(rb.position + movement * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isColliding || health <= 0)
        {
            return;
        }

        if (other.tag == "Wall")
        {
            movement *= -1;
            isColliding = true;
        }

        if (other.tag == "Sharp")
        {
            movement *= 0.75f;
            health--;

            other.gameObject.GetComponent<Interactable>().DisablePhysics();
            other.gameObject.transform.parent = transform;
            other.gameObject.GetComponent<Interactable>().pickedUp = true;
        }
    }
}
