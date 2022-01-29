using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;

    // Update is called once per frame
    void Update()
    {
        // Get movement vectors for moving the player
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Get mouse position for rotating the player
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // Reload scene whenever R is pushed
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (GetComponent<PlayerThrowing>().bluntObject != null && GetComponent<PlayerThrowing>().bluntObject.GetComponent<Interactable>().isHeavy)
        {
            moveSpeed = 2.5f;
        }
        else
        {
            moveSpeed = 5f;
        }
    }

    void FixedUpdate()
    {
        // Move the player
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        // Rotate the player
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    // public void ApplySlow()
    // {
    //     moveSpeed *= 0.5f;
    // }

    // public void RevertSlow()
    // {
    //     moveSpeed *= 2f;
    // }
}
