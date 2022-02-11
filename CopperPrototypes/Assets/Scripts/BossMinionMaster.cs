using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMinionMaster : MonoBehaviour
{

    public GameObject[] phaseTwoEnemies;

    public GameObject[] phaseThreeEnemies;

    public GameObject[] phaseTwoDoors;

    public GameObject[] phaseThreeDoors;

    void Start()
    {

        //
        // Hide all enemies
        //

        foreach (GameObject enemy in phaseTwoEnemies)
        {
            enemy.SetActive(false);
        }

        foreach (GameObject enemy in phaseThreeEnemies)
        {
            enemy.SetActive(false);
        }
    }

    void Update()
    {
        
    }

    public void InitiatePhaseTwo()
    {
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
