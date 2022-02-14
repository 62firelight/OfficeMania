using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowing : MonoBehaviour
{

    public Sprite normalSprite;

    public Sprite carryingSprite;

    public Sprite heavySprite;

    public Transform lightObjectPoint;

    public Transform heavyObjectPoint;

    public float force = 40f;

    public Interactable nearestObject;

    public Interactable nearestSharp;

    public GameObject bluntObject = null;

    public GameObject heldObject = null;

    public GameObject currentObject = null;

    public float mag;

    private SpriteRenderer sr;

    float startTime = 0f;

    void Start()
    {   
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = normalSprite;
    }

    // Update is called once per frame
    void Update()
    {
        // Determine currently held object
        if (bluntObject != null)
        {
            currentObject = bluntObject;
        }
        else
        {
            currentObject = heldObject;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            startTime = Time.time;
        }

        if (Input.GetButton("Fire1"))
        {
            mag = Time.time - startTime;
            mag = Mathf.Clamp(mag, 0.5f, 1.5f);
        }

        // Throw an object
        if (Input.GetButtonUp("Fire1") && currentObject != null)
        {
            // Calculate magnitude of throw force
            mag = Time.time - startTime;
            mag = Mathf.Clamp(mag, 0.5f, 1.5f);
            Debug.Log("Key held down for " + (Time.time - startTime).ToString("F2") + "s for " + force * mag + " force");

            // Change the state of the player's currently held objects
            if (currentObject.GetComponent<Interactable>().isHeavy == true)
            {
                bluntObject = null;

                // Re-enable collision between player and heavy object
                Physics2D.IgnoreCollision(currentObject.gameObject.transform.GetChild(1).GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
            }
            else
            {
                currentObject.gameObject.transform.position = transform.position;
                heldObject = null;
            }
            
            // Throw object using calculated force
            currentObject.GetComponent<Interactable>().Throw(heavyObjectPoint, force * mag);

            currentObject = null;
            sr.sprite = normalSprite;
        }

        // Right-click near a blunt object to pick it up
        if (Input.GetButtonDown("Fire2") && nearestObject != null && nearestObject.gameObject.tag == "Blunt" && AIMaster.takenObjects.Contains(nearestObject.gameObject) == false) 
        {
            if (heldObject != null)
            {
                heldObject.GetComponent<Interactable>().Drop();
                heldObject = null;
            }

            nearestObject.RegisterPickUp();
        }

        if (nearestObject != null && bluntObject == null && currentObject == null && nearestObject.thrownFlag != 2 && nearestObject.damageable == false)// && nearestObject.rb.isKinematic == true && nearestObject.pickedUp == false)
        {
            if (nearestObject.isHeavy == false && nearestObject.pickedUp == false)
            {
                nearestObject.RegisterPickUp();
            }
        }
    }

    public void PickUp(Transform obj)
    {
        // Add object to player's inventory, then 
        // change object position to show that the player is holding it
        obj.parent = transform;
        if (obj.gameObject.GetComponent<Interactable>().isHeavy == true)
        {
            sr.sprite = heavySprite;
            bluntObject = obj.gameObject;
            
            obj.localPosition = heavyObjectPoint.localPosition;

            // Disable collision between player and heavy object while player is holding it
            Physics2D.IgnoreCollision(obj.GetChild(1).GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        else
        {
            sr.sprite = carryingSprite;
            obj.position = lightObjectPoint.position;

            heldObject = obj.gameObject;
        }
        obj.rotation = transform.rotation;
        obj.Translate(0, 0, -1);
    }

    /**void OnGUI()
    {
        GUI.skin.label.fontSize = 72;
        GUI.skin.label.alignment = TextAnchor.UpperLeft;

        GUI.Label(new Rect(0, 0, Camera.main.pixelWidth, Camera.main.pixelHeight), GetComponent<PlayerDamage>().playerHealth + " Health);
    }**/
}
