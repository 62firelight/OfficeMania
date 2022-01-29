using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float force = 20f;

    public Interactable nearestObject;

    public GameObject carrying = null;

    public int sharpCount = 0;

    float startTime = 0f;

    public void PickUp(Transform item)
    {
        if (carrying != null)
        {
            return;
        }

        carrying = item.gameObject;

        if (item.gameObject.tag == "Sharp")
        {
            sharpCount++;
        }
        
        item.parent = transform;
        item.position = transform.position;
        item.rotation = transform.rotation;
        item.Translate(0, 0, -1);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            startTime = Time.time;
        }

        if (Input.GetButtonUp("Fire1") && carrying != null)
        {
            float mag = Time.time - startTime;

            mag = Mathf.Clamp(mag, 0.5f, 1.5f);

            Debug.Log("Key held down for " + (Time.time - startTime).ToString("F2") + "s for " + force * mag + " force");

            carrying.GetComponent<Interactable>().Throw(firePoint, force * mag);

            if (carrying.tag == "Sharp")
            {
                sharpCount--;
            }

            carrying = null;
        }

        if (nearestObject != null && Input.GetButtonDown("Fire2"))
        {
            nearestObject.RegisterPickUp();
        }
    }
}
