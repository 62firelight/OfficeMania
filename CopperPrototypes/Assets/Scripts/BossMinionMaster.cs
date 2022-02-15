using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMinionMaster : MonoBehaviour
{

    public GameObject[] phaseTwoEnemies;

    public GameObject[] phaseTwoDoors;

    public GameObject[] phaseTwoObjects;

    public GameObject[] phaseThreeEnemies;

    public GameObject[] phaseThreeDoors;

    public GameObject[] phaseThreeObjects;

    void Start()
    {

        //
        // Hide all associated enemies and objects
        //

        // Phase 2 enemies and objects
        foreach (GameObject enemy in phaseTwoEnemies)
        {
            enemy.SetActive(false);
        }
        foreach (GameObject obj in phaseTwoObjects)
        {
            obj.SetActive(false);
        }

        // Phase 3 enemies and objects
        foreach (GameObject enemy in phaseThreeEnemies)
        {
            enemy.SetActive(false);
        }
        foreach (GameObject obj in phaseThreeObjects)
        {
            obj.SetActive(false);
        }
    }

    void Update()
    {
        
    }

    public void InitiatePhaseTwo()
    {
        foreach (GameObject obj in phaseTwoObjects)
        {
            obj.SetActive(true);
        }

        foreach (GameObject door in phaseTwoDoors)
        {
            door.SetActive(false);
        }

        foreach (GameObject enemy in phaseTwoEnemies)
        {
            AStarPathfinder pathfinder = enemy.GetComponent<AStarPathfinder>();
            pathfinder.ResetTimeKeeper();

            enemy.SetActive(true);
        }

    }

    public void InitiatePhaseThree()
    {
        foreach (GameObject obj in phaseThreeObjects)
        {
            obj.SetActive(true);
        }

        foreach (GameObject door in phaseThreeDoors)
        {
            door.SetActive(false);
        }

        foreach (GameObject enemy in phaseThreeEnemies)
        {
            AStarPathfinder pathfinder = enemy.GetComponent<AStarPathfinder>();
            pathfinder.ResetTimeKeeper();

            enemy.SetActive(true);
        }
    }
}
