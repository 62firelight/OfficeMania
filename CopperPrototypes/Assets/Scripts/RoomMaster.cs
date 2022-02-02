using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AStarGrid))]
public class RoomMaster : MonoBehaviour
{
    public Enemy[] enemies;

    public GameObject[] entryDoors;

    public GameObject exitDoor;

    private bool roomClear = false;

    public bool aiEnabled = false;

    public bool seePlayer = false;

    public Transform bottomLeftCorner;

    public Transform topRightCorner;

    private AStarGrid grid;

    void Awake()
    {
        grid = GetComponent<AStarGrid>();
        // grid.enabled = false;

        bottomLeftCorner = transform.GetChild(0);
        topRightCorner = transform.GetChild(1);

        grid.MapStartPosition = bottomLeftCorner.position;
        grid.MapEndPosition = topRightCorner.position;

        foreach (Enemy enemy in enemies)
        {
            Debug.Log(enemy);
            GameObject enemyObj = enemy.gameObject;

            AStarPathfinder enemyPathfinder = enemyObj.GetComponent<AStarPathfinder>();
            enemyPathfinder.gridObject = gameObject;

            Chase enemyChase = enemyObj.GetComponent<Chase>();
            enemyChase.roomMaster = gameObject;
        }
    }

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
        if (aiEnabled && roomClear == false && exitDoor != null)
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
                //Destroy(exitDoor);
                exitDoor.SetActive(false);

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

            // transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (roomClear == true && collision.tag == "Player")
        {
            Debug.Log("Disabling AI...");
            aiEnabled = false;

            // transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void SetSeePlayer(bool status)
    {
        seePlayer = status;
    }
}
