using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Chase))]
public class Throw : MonoBehaviour
{

    Chase chase;

    GameObject target;

    public GameObject currentObject = null;

    public Sprite normalSprite;

    public Sprite carryingSprite;

    public Sprite heavySprite;

    public Sprite aimSprite;

    public Sprite throwSprite;

    public GameObject player;

    public Transform lightObjectPoint;

    public Transform heavyObjectPoint;

    public Transform aimPoint;

    public GameObject mostRecentObject = null;

    public bool isElite = false;

    float delay;

    private SpriteRenderer sr;

    private bool aimBlunt = false;

    private bool aimHeavy = false;

    // Start is called before the first frame update
    void Start()
    {
        chase = GetComponent<Chase>();
        player = GameObject.FindGameObjectWithTag("Player");

        // Find the closest object to pick up
        if (chase.target == null)
        {
            GameObject closestObject = GetClosestObject(mostRecentObject);

            if (closestObject != null)
            {
                chase.target = closestObject;
            }
        }

        sr = GetComponent<SpriteRenderer>();
        sr.sprite = normalSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (delay >= 0)
        {
            delay -= Time.deltaTime;
        }

        if (GetComponent<Enemy>().health <= 0 || GetComponent<Chase>().seePlayer == false)
		{
			return;
		}

        if (mostRecentObject != null && delay <= 1)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), mostRecentObject.GetComponent<Collider2D>(), false);
            mostRecentObject = null;
        }

        target = chase.target;

        // If the player got to the object I want first, find another object
        if (chase.target == player.GetComponent<PlayerThrowing>().currentObject || chase.target == player.GetComponent<PlayerThrowing>().heldObject ||
            AIMaster.takenObjects.Contains(chase.target) == true)
        {
            chase.target = GetClosestObject(mostRecentObject);
        }

        if (currentObject != null && currentObject.transform.parent == transform && delay < 1)
        {
            if (currentObject.GetComponent<Interactable>().isHeavy == false)
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, player.transform.position - transform.position);
                Debug.DrawRay(transform.position, player.transform.position - transform.position);

                bool playerSeen = false;
                // int enemiesNearby = 0;
                foreach (RaycastHit2D hit in hits)
                {
                    // if (hit.collider != GetComponent<Collider2D>() && hit.collider != GetComponentInChildren<CircleCollider2D>() 
                    //     && hit.transform.gameObject.tag == "Enemy" && hit.transform.gameObject.name != "Vision")
                    // {
                    //     // Debug.Log(GetComponentInChildren<CircleCollider2D>());
                    //     // Debug.Log(hit.collider);
                    //     Debug.Log(hit.transform.gameObject.name);
                    //     playerSeen = false;
                    //     break;
                    // }

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

                if (playerSeen == true)
                {
                    sr.sprite = aimSprite;
                    currentObject.transform.position = aimPoint.transform.position;

                    if (aimBlunt == false && currentObject.tag == "Blunt")
                    {
                        currentObject.transform.Rotate(new Vector3(0, 0, -45));
                        aimBlunt = true;
                    }
                }
            }
            else if (aimHeavy == false)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, target.transform.position);

                if (distanceToPlayer < 6f)
                {
                    currentObject.transform.Translate(0, -0.25f, -1);
                    aimHeavy = true;
                }
            }
        }

        // Throw an object if we are holding it for at least 2 seconds
        if (currentObject != null && currentObject.transform.parent == transform && delay <= 0)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, player.transform.position - transform.position);
            Debug.DrawRay(transform.position, player.transform.position - transform.position);

            bool playerSeen = false;
            // int enemiesNearby = 0;
            foreach (RaycastHit2D hit in hits)
            {
                // if (hit.collider != GetComponent<Collider2D>() && hit.collider != GetComponentInChildren<CircleCollider2D>() 
                //     && hit.transform.gameObject.tag == "Enemy" && hit.transform.gameObject.name != "Vision")
                // {
                //     // Debug.Log(GetComponentInChildren<CircleCollider2D>());
                //     // Debug.Log(hit.collider);
                //     Debug.Log(hit.transform.gameObject.name);
                //     playerSeen = false;
                //     break;
                // }

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
                delay = 1.5f;

                if (currentObject.GetComponent<Interactable>().isHeavy == false)
                {
                    sr.sprite = carryingSprite;
                    currentObject.transform.position = lightObjectPoint.transform.position;
                }
                else
                {
                    currentObject.transform.position = heavyObjectPoint.transform.position;
                }

                return;
            }

            Interactable obj = currentObject.GetComponent<Interactable>();

            // If I'm an elite guard with a heavy object
            if (obj.isHeavy == true && isElite == true)
            {

                // Delay the throw until we are close enough to the player
                // (act as if we are blocking the player's thrown objects)
                float distanceToPlayer = Vector2.Distance(transform.position, target.transform.position);
                if (distanceToPlayer < 4.5f)
                {
                    currentObject.transform.Translate(0, 0.25f, 1);
                    currentObject.GetComponent<Interactable>().EnemyThrow(transform, 30, gameObject);
                    aimHeavy = false;
                }
                else
                {
                    return;
                }
            }
            else
            {
                currentObject.transform.Rotate(new Vector3(0, 0, -90));

                currentObject.transform.position = transform.position;
                currentObject.GetComponent<Interactable>().EnemyThrow(transform, 30, gameObject);
                aimBlunt = false;
            }
           
            mostRecentObject = currentObject;
            currentObject = null;

            delay = 2;
            sr.sprite = normalSprite;

            GameObject closestObject = GetClosestObject(mostRecentObject);

            if (closestObject != null)
            {
                chase.target = closestObject;
            }
        }

        if (target == null || (target == player && currentObject == null))
        {
            GameObject closestObject = GetClosestObject(mostRecentObject);

            if (closestObject != null)
            {
                chase.target = closestObject;
            }
            return;
        }

        // Pick up object if it's close enough
        if (target.tag != "Player")
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance < 2f && delay <= 0)
            {
                Debug.Log(gameObject.name + " picks up " + chase.target);
                EnemyPickUp(chase.target.transform);
            }
        }
    }

    void EnemyPickUp(Transform item)
    {
        chase.target = player;

        currentObject = item.gameObject;

        item.gameObject.GetComponent<Interactable>().RegisterEnemyPickUp();
        item.parent = transform;

        if (item.gameObject.GetComponent<Interactable>().isHeavy)
        {
            sr.sprite = heavySprite;
            item.position = heavyObjectPoint.position;
        }
        else
        {
            sr.sprite = carryingSprite;
            item.position = lightObjectPoint.position;
        }

        item.rotation = transform.rotation;
        //item.Translate(0, 0, -1);

        //if (item.gameObject.GetComponent<Interactable>().isHeavy == false)
        delay = 2;

        if (player.GetComponent<PlayerThrowing>().bluntObject != null)
        {
            delay = 1.25f;
        }

        if (item.gameObject.GetComponent<Interactable>().isHeavy == true)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, target.transform.position);

            if (distanceToPlayer < 2f)
            {
                delay = 0;
            }
        }

        if (item.gameObject.tag == "Blunt" && item.GetComponent<Interactable>().isHeavy == false)
        {
            item.Rotate(new Vector3(0, 0, 90));
        }
    }

    GameObject GetClosestObject(GameObject ignoredObject)
    {
        int numberOfObjects = 0;

        List<GameObject> detectedObjects = null;
        if (chase.roomMaster != null)
        {
            RoomMaster roomMaster = chase.roomMaster.GetComponent<RoomMaster>();
            
            detectedObjects = roomMaster.GetDetectedObjects();
            numberOfObjects = detectedObjects.Count;
        }
        
        GameObject[] objects = null;
        if (detectedObjects == null || detectedObjects.Count <= 0)
        {
            objects = GameObject.FindGameObjectsWithTag("Blunt");
            numberOfObjects = objects.Length;
        }

        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;
        float distance;

        for (int i = 0; i < numberOfObjects; i++)
        {
            GameObject obj;
            if (detectedObjects != null && detectedObjects.Count > 0)
            {
                obj = detectedObjects[i];
            }
            else
            {
                obj = objects[i];
            }

            if (obj == ignoredObject)
            {
                continue;
            }

            Interactable objInteract = obj.GetComponent<Interactable>();
            PlayerThrowing playerThrowing = player.GetComponent<PlayerThrowing>();

            // Ignore objects held by other enemies
            if (AIMaster.takenObjects.Contains(obj))
            {
                continue;
            }

            // Ignore player held objects
            if ((isElite == false && objInteract.isHeavy) || obj == playerThrowing.currentObject || obj == playerThrowing.heldObject)
            {
                continue;
            }

            // Find distance to object
            distance = Vector2.Distance(transform.position, obj.transform.position);

            // If the distance to the object is closer than the known closest distance, 
            // then this object must be closest
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }
        }

        return closestObject;
    }
}
