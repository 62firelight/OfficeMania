using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if (collision.tag == "Player")
        {
            // SceneManager.LoadScene("MainMenu");

            AIMaster.currentLevel++;

            int currentLevel = AIMaster.currentLevel;
            //Debug.Log(currentLevel);
            StartCoroutine(LoadLevel(currentLevel));
        }
    }

    IEnumerator LoadLevel(int levelIndex) {

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
