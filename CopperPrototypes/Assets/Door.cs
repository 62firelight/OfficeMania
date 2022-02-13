using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;

    Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponentInChildren<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CloseDoor();
        }
        
        if (Input.GetKeyDown(KeyCode.O))
        {
            OpenDoor();
        }
    }

    public void CloseDoor()
    {
        coll.enabled = true;

        anim.ResetTrigger("DoorOpen");

        anim.SetTrigger("DoorClosed");
    }

    public void OpenDoor()
    {
        coll.enabled = false;

        anim.ResetTrigger("DoorClosed");

        anim.SetTrigger("DoorOpen");
    }
}
