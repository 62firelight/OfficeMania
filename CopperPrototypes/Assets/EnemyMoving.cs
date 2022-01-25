using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    public Vector2 movement;

    private Rigidbody2D rb;

    private bool isColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (movement == Vector2.zero)
        {
            movement = new Vector3(2f, 0, 0);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isColliding = false;
        rb.MovePosition(rb.position + movement * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall" && isColliding == false)
        {
            movement *= -1;
            isColliding = true;
        }
    }
}
