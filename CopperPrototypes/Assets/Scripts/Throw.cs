using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Chase))]
public class Throw : MonoBehaviour
{

    Chase chase;

    GameObject target;

    GameObject carrying = null;

    public GameObject player;

    GameObject mostRecentObject = null;

    float delay;

    // Start is called before the first frame update
    void Start()
    {
        chase = GetComponent<Chase>();

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
        target = chase.target;

        if (delay >= 0)
        {
            delay -= Time.deltaTime;
        }

        if (carrying != null && delay <= 0)
        {
            carrying.GetComponent<Interactable>().EnemyThrow(transform, 20, gameObject);
            mostRecentObject = carrying;
            carrying = null;

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

        float distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance < 1 && delay <= 0)
        {
            EnemyPickUp(chase.target.transform);
        }

    }

    void EnemyPickUp(Transform item)
    {
        chase.target = player;

        carrying = item.gameObject;

        item.gameObject.GetComponent<Interactable>().RegisterPickUp();
        item.parent = transform;
        item.position = transform.position;
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

            distance = Vector2.Distance(transform.position, obj.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }
        }

        return closestObject;
    }
}
