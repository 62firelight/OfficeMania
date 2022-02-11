using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    // private void Start()
    // {
    //     AIMaster.currentLevel = 0;
    // }

    // This is the start button on the main menu
    public void StartGame() { 
        // This will take you to the starting scene
        SceneManager.LoadScene("PrototypeLevel");

    }

    // This is the controls button on the main menu
    public void ShowControls() {
        // This will show the user the controls
        SceneManager.LoadScene("Help");
    }

    //This is the quit button on the main menu
    public void QuitGame() {
        // This will quit the application
        Application.Quit();
    }

    public void MenuScreen() {
        //This will return to the main menu
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel() {
        int sceneIndex = AIMaster.currentLevel;
        SceneManager.LoadScene(sceneIndex);
        Debug.Log(sceneIndex);
    }
}
