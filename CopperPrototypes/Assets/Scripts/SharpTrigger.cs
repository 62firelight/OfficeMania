using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpTrigger : MonoBehaviour
{

    public bool playerNear = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerNear = false;
        }
    }
}
