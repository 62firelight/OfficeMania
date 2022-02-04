using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Chase))]
public class Throw : MonoBehaviour
{

    Chase chase;

    GameObject target;

    GameObject currentObject = null;

    public GameObject player;

    public Transform firePoint;

    public GameObject mostRecentObject = null;

    float delay;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (delay >= 0)
        {
            delay -= Time.deltaTime;
        }

        if (GetComponent<Enemy>().health <= 0)
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

        // Throw an object if we are holding it for at least 2 seconds
        if (currentObject != null && delay <= 0)
        {
            currentObject.GetComponent<Interactable>().EnemyThrow(transform, 30, gameObject);
            mostRecentObject = currentObject;
            currentObject = null;

            delay = 2;

            GameObject closestObject = GetClosestObject(mostRecentObject);

            if (closestObject != null)
            {
                chase.target = closestObject;
            }
        }

        if (target == null)
        {
            return;
        }

        // Pick up object if it's close enough
        float distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance < 1f && delay <= 0)
        {
            Debug.Log(gameObject.name + " picks up " + chase.target);
            EnemyPickUp(chase.target.transform);
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
            item.position = firePoint.position;
        }
        else
        {
            item.position = transform.position;
        }

        item.rotation = transform.rotation;
        item.Translate(0, 0, -1);

        delay = 1;
    }

    GameObject GetClosestObject(GameObject ignoredObject)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Blunt");

        GameObject closestObject = null;
        float closestDistance = 1000000;
        float distance;

        foreach (GameObject obj in objects)
        {
            if (obj == ignoredObject)
            {
                continue;
            }

            // Ignore player held objects
            if (obj == player.GetComponent<PlayerThrowing>().currentObject || obj == player.GetComponent<PlayerThrowing>().heldObject)
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
