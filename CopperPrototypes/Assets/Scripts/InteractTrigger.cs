using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTrigger : MonoBehaviour
{

    private Interactable parent;

    void Start()
    {
        parent = GetComponentInParent<Interactable>();

        if (parent == null)
        {
            Debug.Log("ERROR: Parent of " + gameObject.name + " was null!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (parent.isHeavy == false)
            {
                other.GetComponent<PlayerThrowing>().nearestObject = parent;
            }
            else
            {
                other.GetComponent<PlayerThrowing>().nearestHeavy = parent;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerThrowing>().nearestObject == null)
            {
                other.GetComponent<PlayerThrowing>().nearestObject = parent;
            }

            if (parent.isHeavy == true && other.GetComponent<PlayerThrowing>().nearestHeavy == null)
            {
                other.GetComponent<PlayerThrowing>().nearestHeavy = parent;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (parent.isHeavy == false)
            {
                other.GetComponent<PlayerThrowing>().nearestObject = null;
            }
            else 
            {
                other.GetComponent<PlayerThrowing>().nearestHeavy = null;
            }
        }
    }
}
