using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AStarGrid))]
public class RoomMaster : MonoBehaviour
{
    public Enemy[] enemies;

    public GameObject[] entryDoors;

    public GameObject[] exitDoors;

    private bool roomClear = false;

    public bool aiEnabled = false;

    public bool seePlayer = false;

    public Transform bottomLeftCorner;

    public Transform topRightCorner;

    private AStarGrid grid;

    void Awake()
    {
        grid = GetComponent<AStarGrid>();

        // Set grid position
        bottomLeftCorner = transform.GetChild(0);
        topRightCorner = transform.GetChild(1);
        grid.MapStartPosition = bottomLeftCorner.position;
        grid.MapEndPosition = topRightCorner.position;

        if (enemies != null && enemies.Length > 0)
        {
            // Set components for each enemy
            foreach (Enemy enemy in enemies)
            {
                GameObject enemyObj = enemy.gameObject;

                // Set grid object for enemy pathfinder
                AStarPathfinder enemyPathfinder = enemyObj.GetComponent<AStarPathfinder>();
                enemyPathfinder.gridObject = gameObject;

                // Set room master for enemy chase component
                Chase enemyChase = enemyObj.GetComponent<Chase>();
                enemyChase.roomMaster = gameObject;
            }
        }
        
    }

    void Start()
    {
        foreach (GameObject entryDoor in entryDoors)
        {
            entryDoor.SetActive(false);
        }
    }

    void Update()
    {
        if (aiEnabled && roomClear == false)
        {
            // Assume all enemies are clear unless we know otherwise
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
                if (exitDoors != null && exitDoors.Length > 0)
                {
                    foreach(GameObject exitDoor in exitDoors)
                    {
                        exitDoor.SetActive(false);
                    }
                }

                if (entryDoors != null && entryDoors.Length > 0)
                {
                    foreach (GameObject entryDoor in entryDoors)
                    {
                        entryDoor.SetActive(false);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (roomClear == false && collision.tag == "Player")
        {
            aiEnabled = true;

            if (entryDoors.Length > 0)
            {
                // Seal all entry doors so the player can't exit the room
                foreach (GameObject entryDoor in entryDoors)
                {
                    entryDoor.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (roomClear == true && collision.tag == "Player")
        {
            aiEnabled = false;
        }
    }

    public void SetSeePlayer(bool status)
    {
        seePlayer = status;
    }
}
