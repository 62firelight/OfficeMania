using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTrigger : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerThrowing>().nearestObject = GetComponentInParent<Interactable>();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerThrowing>().nearestObject == null)
            {
                other.GetComponent<PlayerThrowing>().nearestObject = GetComponentInParent<Interactable>();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerThrowing>().nearestObject = null;
        }
    }
}
