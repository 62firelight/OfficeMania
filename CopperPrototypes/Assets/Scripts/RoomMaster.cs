using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMaster : MonoBehaviour
{
    public Enemy[] enemies;

    public GameObject door;

    public GameObject[] entryDoors;

    private bool roomClear = false;

    public bool aiEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject entryDoor in entryDoors)
        {
            entryDoor.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (aiEnabled && door != null)
        {
            // Assume all enemies unless we know otherwise
            bool enemiesClear = true;

            // Check if room has no enemies conscious
            for (int i = 0; i < enemies.Length; i++)
            {
                Enemy enemy = enemies[i];

                // If one enemy is conscious, then room can't be clear
                if (enemy.health > 0)
                {
                    enemiesClear = false;
                    break;
                }
            }

            // If enemies are clear, then room must be clear
            if (enemiesClear)
            {
                roomClear = true;
            }

            // Open all doors to the room so the player can exit
            if (roomClear)
            {
                Destroy(door);

                foreach (GameObject entryDoor in entryDoors)
                {
                    entryDoor.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (roomClear == false && collision.tag == "Player")
        {
            aiEnabled = true;

            foreach (GameObject entryDoor in entryDoors)
            {
                entryDoor.SetActive(true);
            }

            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            aiEnabled = false;

            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
