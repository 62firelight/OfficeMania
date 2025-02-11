using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour
{
    PlayerKeycard playerKeycard;

    public GameObject keyDisplay;

    public AudioClip pickupSound;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.Log(gameObject.name + " can't find player!");
            return;
        }
        
        playerKeycard = player.GetComponent<PlayerKeycard>();

        if (playerKeycard == null)
        {
            Debug.Log(gameObject.name + " can't find PlayerKeycard component!");
            return;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (pickupSound != null) AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            playerKeycard.SetHasKeycard(true);

            keyDisplay.SetActive(true);
            Destroy(gameObject);
        }
    }
}
