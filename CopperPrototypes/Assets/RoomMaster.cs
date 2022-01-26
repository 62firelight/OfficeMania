using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMaster : MonoBehaviour
{
    public Transform[] enemies;

    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (door.activeInHierarchy == true)
        {
            bool roomClear = true;

            // Check if room has no enemies conscious
            for (int i = 0; i < enemies.Length; i++)
            {
                GameObject enemy = enemies[i].gameObject;

                // If one enemy is conscious, then room can't be clear
                if (enemy.GetComponent<Enemy>().health > 0)
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
