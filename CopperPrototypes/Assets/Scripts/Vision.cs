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
            // Do a raycast to see if the player is hiding behind a wall
            GameObject player = other.gameObject;
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, player.transform.position - transform.position);
            Debug.DrawRay(transform.position, player.transform.position - transform.position);

            bool playerSeen = false;
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != GetComponent<Collider2D>() && hit.transform.gameObject.tag == "Wall" && hit.transform.gameObject.layer != 7)
                {
                    playerSeen = false;
                    break;
                }

                if (hit.collider != null && hit.transform.gameObject.tag == "Player")
                {
                    playerSeen = true;
                    break;
                }
            }

            if (!playerSeen)
            {
                return;
            }
            
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