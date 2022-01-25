using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoor : MonoBehaviour
{
    // On collision with a trigger collider...
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check the tag of the object the player
        // has collided with
        if (other.tag == "Player")
        {
            // Touching the door
            Destroy(other.gameObject);
            
        }
    }
}
