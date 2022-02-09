using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if (collision.tag == "Player")
        {
            // SceneManager.LoadScene("MainMenu");

            AIMaster.currentLevel++;

            int currentLevel = AIMaster.currentLevel;
            Debug.Log(currentLevel);
            SceneManager.LoadScene(currentLevel);
        }
    }
}
