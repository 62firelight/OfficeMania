using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Vision : MonoBehaviour
{

    RoomMaster roomMaster;
    Dialogue dialogue;

    // Use this for initialization
    void Start()
    {
        roomMaster = GetComponentInParent<Chase>().roomMaster.GetComponent<RoomMaster>();

        dialogue = transform.parent.GetComponentInChildren<Dialogue>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && roomMaster.seePlayer == false)
        {
            roomMaster.SetSeePlayer(true);
            dialogue.DisplayDialogue(DialogueMaster.GetPlayerSeenLine());
        }
    }
}