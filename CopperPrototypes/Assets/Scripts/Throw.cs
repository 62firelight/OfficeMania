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

    float delay;

    // Start is called before the first frame update
    void Start()
    {
        chase = GetComponent<Chase>();
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
            Debug.Log("Enemy threw object");
            carrying.GetComponent<Interactable>().EnemyThrow(transform, 20, gameObject);
            carrying = null;

            delay = 2;

            GameObject closestObject = GetClosestObject();

            if (closestObject != null)
            {
                chase.target = closestObject;
            }
        }

        if (target == null)
        {
            return;
        }

        Debug.Log("Enemy is chasing " + target.name);

        float distance = Vector2.Distance(transform.position, target.transform.position);
        Debug.Log("Enemy is " + distance + " units away from " + target.name);

        if (distance < 0.5 && delay <= 0)
        {
            EnemyPickUp(chase.target.transform);
        }

    }

    void EnemyPickUp(Transform item)
    {
        Debug.Log("Enemy has picked up " + item);
        chase.target = player;

        carrying = item.gameObject;

        item.gameObject.GetComponent<Interactable>().RegisterPickUp();
        item.parent = transform;
        item.position = transform.position;
        item.rotation = transform.rotation;
        item.Translate(0, 0, -1);

        delay = 1;
    }

    GameObject GetClosestObject()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Blunt");

        GameObject closestObject = null;
        float closestDistance = 1000000;
        float distance;

        foreach (GameObject obj in objects)
        {
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
