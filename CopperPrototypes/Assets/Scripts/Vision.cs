using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Vision : MonoBehaviour
{

    RoomMaster roomMaster;
    Dialogue dialogue;

    public bool isBoss = false;

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

            if (isBoss == false)
            {
                dialogue.DisplayDialogue(DialogueMaster.GetPlayerSeenLine());
            }
            else
            {
                DynamicMusic music = transform.parent.gameObject.GetComponent<DynamicMusic>();

                if (music == null)
                {
                    Debug.Log("Could not find DynamicMusic component for " + gameObject.name + "!");
                    return;
                }

                music.PlayBossMusic();
            }
        }
    }
}