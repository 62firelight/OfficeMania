using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float force = 20f;

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
                    GetComponent<PlayerMovement>().RevertSlow();
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
        }
        else
        {
            item.position = transform.position;
        }

        item.rotation = transform.rotation;
        
        item.Translate(0, 0, -1);
    }
}
