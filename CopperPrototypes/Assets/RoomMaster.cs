using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMaster : MonoBehaviour
{
    public Enemy[] enemies;

    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (door != null)
        {
            bool roomClear = true;

            // Check if room has no enemies conscious
            for (int i = 0; i < enemies.Length; i++)
            {
                Enemy enemy = enemies[i];

                // If one enemy is conscious, then room can't be clear
                if (enemy.health > 0)
                {
                    roomClear = false;
                    break;
                }
            }

            if (roomClear)
            {
                Destroy(door);
            }
        }
    }
}
