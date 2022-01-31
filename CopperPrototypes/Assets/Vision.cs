using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{

    RoomMaster roomMaster;

    // Use this for initialization
    void Start()
    {
        roomMaster = GetComponentInParent<Chase>().roomMaster.GetComponent<RoomMaster>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            roomMaster.SetSeePlayer(true);
        }
    }
}