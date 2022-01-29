using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowing : MonoBehaviour
{

    public Transform firePoint;

    public float force = 40f;

    public Interactable nearestObject;

    public Interactable nearestSharp;

    public GameObject bluntObject = null;

    public GameObject currentObject = null;

    public Queue<GameObject> sharpObjects = new Queue<GameObject>();

    float startTime = 0f;

    // Update is called once per frame
    void Update()
    {
        // Determine currently held object
        if (bluntObject != null)
        {
            currentObject = bluntObject;
        }
        else if (sharpObjects.Count > 0)
        {
            currentObject = sharpObjects.Peek();
        }
        else
        {
            currentObject = null;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            startTime = Time.time;
        }

        // Throw an object
        if (Input.GetButtonUp("Fire1") && currentObject != null)
        {
            float mag = Time.time - startTime;

            mag = Mathf.Clamp(mag, 0.5f, 1.5f);

            Debug.Log("Key held down for " + (Time.time - startTime).ToString("F2") + "s for " + force * mag + " force");

            currentObject.GetComponent<Interactable>().Throw(firePoint, force * mag);

            if (currentObject.tag == "Sharp")
            {
                sharpObjects.Dequeue();
            }
            else
            {
                if (bluntObject.GetComponent<Interactable>().isHeavy)
                {
                    // Revert player movement speed back to normal
                    GetComponent<PlayerMovement>().RevertSlow();

                    // Re-enable collision between player and heavy object
                    Physics2D.IgnoreCollision(nearestObject.gameObject.transform.GetChild(1).GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
                }

                bluntObject = null;
            }
        }

        // Right-click near a blunt object to pick it up
        if (Input.GetButtonDown("Fire2") && nearestObject != null && nearestObject.gameObject.tag == "Blunt")
        {
            nearestObject.RegisterPickUp();
        }
    }

    public void PickUp(Transform item)
    {
    
        if (item.gameObject.tag == "Sharp")
        {
            sharpObjects.Enqueue(item.gameObject);
        }
        else
        {
            bluntObject = item.gameObject;
        }
        
        item.parent = transform;

        if (item.gameObject.GetComponent<Interactable>().isHeavy)
        {
            
            item.localPosition = firePoint.localPosition;

            // Slow down player movement by 50%
            GetComponent<PlayerMovement>().ApplySlow();

            // Disable collision between player and heavy object while player is holding it
            Physics2D.IgnoreCollision(item.GetChild(1).GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        else
        {
            item.position = transform.position;
        }

        item.rotation = transform.rotation;
        
        item.Translate(0, 0, -1);
    }
}
