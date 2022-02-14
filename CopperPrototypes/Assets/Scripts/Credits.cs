using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{

    public GameObject skipButton;

    void Start()
    {
        if (skipButton != null)
        {
            skipButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] creditsTextObjects = GameObject.FindGameObjectsWithTag("CreditsText");

        if (creditsTextObjects.Length <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (skipButton != null && skipButton.activeInHierarchy == false && Input.anyKey)
        {
            skipButton.SetActive(true);
            skipButton.GetComponent<SkipButton>().showTime = 5f;
        }
    }
}
