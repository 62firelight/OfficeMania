using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AStarGrid))]
public class RoomMaster : MonoBehaviour
{
    public Enemy[] enemies;

    public GameObject[] entryDoors;

    public GameObject[] exitDoors;

    private bool roomClear = false;

    public bool aiEnabled = false;

    public bool seePlayer = false;

    public bool levelTwoBattle = false;

    public bool levelTwoBigBattle = false;

    public bool bossBattle = false;

    public Transform bottomLeftCorner;

    public Transform topRightCorner;

    public GameObject mainCamera;

    public GameObject camera;

    public AudioClip doorCloseSound;

    public AudioClip doorOpenSound;

    private AStarGrid grid;

    public bool cameraTransition;

    public bool cameraReset;

    private Vector3 originalCamPosition;

    private float originalSize;

    private float waitTime = 0f;

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
                if (enemy == null)
                {
                    break;
                }

                GameObject enemyObj = enemy.gameObject;

                // Set grid object for enemy pathfinder
                AStarPathfinder enemyPathfinder = enemyObj.GetComponent<AStarPathfinder>();
                enemyPathfinder.gridObject = gameObject;

                // Set room master for enemy chase component
                Chase enemyChase = enemyObj.GetComponent<Chase>();
                enemyChase.roomMaster = gameObject;

                // If enemy can throw, set its player variable
                Throw enemyThrow = enemyObj.GetComponent<Throw>();
                if (enemyThrow != null)
                {
                    enemyThrow.player = GameObject.FindGameObjectWithTag("Player");
                }
            }
        }
        
    }

    void Start()
    {
        if (entryDoors != null && entryDoors.Length > 0)
        {
            foreach (GameObject entryDoor in entryDoors)
            {
                entryDoor.SetActive(false);
            }
        }
        
        if (exitDoors != null && exitDoors.Length > 0)
        {
            foreach (GameObject exitDoor in exitDoors)
            {
                exitDoor.SetActive(false);
            }
        }
        
        mainCamera = Camera.main.gameObject;
        cameraTransition = false;
        cameraReset = false;

        originalCamPosition = mainCamera.transform.position;
        originalSize = mainCamera.GetComponent<Camera>().orthographicSize;
    }

    void Update()
    {
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;

            if (waitTime <= 0)
            {
                SceneManager.LoadScene("Credits");
            }
        }

        if (cameraTransition == true && cameraReset == false)
        {
            mainCamera.GetComponent<SmoothCameraFollow>().enabled = false;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, camera.transform.position, 2.5f * Time.deltaTime);
            mainCamera.GetComponent<Camera>().orthographicSize = Mathf.Lerp(mainCamera.GetComponent<Camera>().orthographicSize, camera.GetComponent<Camera>().orthographicSize, 2.4f * Time.deltaTime);
        }

        if (cameraReset == true)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, originalCamPosition.z);

            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, playerPosition, 2.5f * Time.deltaTime);
            mainCamera.GetComponent<Camera>().orthographicSize = Mathf.Lerp(mainCamera.GetComponent<Camera>().orthographicSize, originalSize, 2.4f * Time.deltaTime);

            float sqrMag = Vector3.SqrMagnitude(playerPosition - mainCamera.transform.position);
            // Debug.Log("1: " + (sqrMag < 0.1f));
            // Debug.Log("2: " + (Mathf.Abs(mainCamera.GetComponent<Camera>().orthographicSize - originalSize) < 0.1f));

            if (sqrMag < 0.1f && Mathf.Abs(mainCamera.GetComponent<Camera>().orthographicSize - originalSize) < 0.1f)
            {
                mainCamera.transform.position = playerPosition;
                mainCamera.GetComponent<Camera>().orthographicSize = originalSize;

                mainCamera.GetComponent<SmoothCameraFollow>().enabled = true;
                cameraTransition = false;
                cameraReset = false;
            }
        }

        if (aiEnabled && roomClear == false)
        {
            // Assume all enemies are clear unless we know otherwise
            bool enemiesClear = true;

            // Check if room has no enemies conscious
            for (int i = 0; i < enemies.Length; i++)
            {
                Enemy enemy = enemies[i];

                // If one enemy is conscious, then room can't be clear
                if (enemy.health > 0 || (enemy.isBoss == true && enemy.bossHealth > 0))
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

                // For level 2, change level music back to normal out-of-combat music
                if (seePlayer == true && SceneManager.GetActiveScene().name == "Level2")
                {
                    GameObject levelMasterObj = GameObject.FindGameObjectWithTag("LevelMaster");

                    LevelMaster levelMaster = levelMasterObj.GetComponent<LevelMaster>();

                    if (levelTwoBattle || levelTwoBigBattle) levelMaster.TriggerLightsOffMusicMid();
                }

                // For the boss, load the main menu
                if (bossBattle)
                {
                    // SceneManager.LoadScene("MainMenu");
                    // SceneManager.LoadScene("Credits");
                    waitTime = 5f;
                }

                if (camera != null) cameraReset = true;

                if (doorOpenSound != null) AudioSource.PlayClipAtPoint(doorOpenSound, Camera.main.transform.position);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (roomClear == false && collision.tag == "Player")
        {
            if (aiEnabled == false && doorCloseSound != null) AudioSource.PlayClipAtPoint(doorCloseSound, Camera.main.transform.position);

            aiEnabled = true;

            if (entryDoors != null && entryDoors.Length > 0)
            {
                // Seal all entry doors so the player can't exit the room
                foreach (GameObject entryDoor in entryDoors)
                {
                    entryDoor.SetActive(true);
                }
            }

            if (exitDoors != null && exitDoors.Length > 0)
            {
                foreach (GameObject exitDoor in exitDoors)
                {
                    exitDoor.SetActive(true);
                }
            }

            if (camera != null) cameraTransition = true;
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

        if (SceneManager.GetActiveScene().name == "Level2")
        {
            GameObject levelMasterObj = GameObject.FindGameObjectWithTag("LevelMaster");

            LevelMaster levelMaster = levelMasterObj.GetComponent<LevelMaster>();

            if (levelTwoBigBattle)
            {
                levelMaster.TriggerBigBattleMusic();
            }
            else if (levelTwoBattle)
            {
                levelMaster.TriggerBattleMusic();
            }
        }
    }

    public List<GameObject> GetDetectedObjects()
    {
        Transform objectDetector = transform.GetChild(2);

        ObjectDetector objectDetectorComponent = objectDetector.gameObject.GetComponent<ObjectDetector>();

        List<GameObject> detectedObjects = objectDetectorComponent.objects;

        return detectedObjects;
    }

    public bool GetRoomClear()
    {
        return roomClear;
    }
}
