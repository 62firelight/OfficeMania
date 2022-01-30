using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{

    public int playerMaxHealth = 3;

    public int playerHealth;

    void Start()
    {
        // Set player health
        playerHealth = playerMaxHealth;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Check if the player collided with an enemy
        if (other.gameObject.tag == "Enemy")
        {
            RegisterDamage();
        }

        // Check if the player collided with a thrown object
        Interactable obj = other.gameObject.GetComponent<Interactable>();

        // If the player has collided, and the object was thrown by an enemy,
        // let the player take damage
        if (obj != null && obj.thrownFlag == 2)
        {
            RegisterDamage();
        }
    }

    void RegisterDamage()
    {
        Debug.Log("Player damaged");
        playerHealth--;

        if (playerHealth <= 0)
        {
            Debug.Log("Player dead");
        }
    }
}
