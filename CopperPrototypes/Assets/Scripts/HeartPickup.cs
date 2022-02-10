using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    private PlayerDamage playerDamage;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerDamage = player.GetComponent<PlayerDamage>();
        }
        else
        {
            Debug.Log(gameObject.name + " can't find player!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Heal the player for half heart
        if (other.tag == "Player" && playerDamage.playerHealth < playerDamage.playerMaxHealth)
        {
            playerDamage.playerHealth++;

            // Heal an extra half heart
            if (playerDamage.playerHealth < playerDamage.playerMaxHealth)
            {
                playerDamage.playerHealth++;
            }

            // Destroy heart object
            gameObject.SetActive(false);
        }
    }

}
