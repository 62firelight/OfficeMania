using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTrigger : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Shooting>().nearestObject = GetComponentInParent<Interactable>();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<Shooting>().nearestObject == null)
            {
                other.GetComponent<Shooting>().nearestObject = GetComponentInParent<Interactable>();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Shooting>().nearestObject = null;
        }
    }
}
