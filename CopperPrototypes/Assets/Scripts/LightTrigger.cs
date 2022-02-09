using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    private LevelMaster levelMaster;

    void Start()
    {
        GameObject levelMasterObj = GameObject.FindGameObjectWithTag("LevelMaster");

        if (levelMasterObj == null)
        {
            Debug.Log(gameObject.name + " can't find LevelMaster!");
        }

        levelMaster = levelMasterObj.GetComponent<LevelMaster>();

        if (levelMaster == null)
        {
            Debug.Log(gameObject.name + " can't find LevelMaster component on LevelMaster game object!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Turn off lights");

        // Darken area and set self to inactive
        Camera.main.backgroundColor /= 2f;
        gameObject.SetActive(false);

        levelMaster.TriggerLightsOffMusic();
    }
}
