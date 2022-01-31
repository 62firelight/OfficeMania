using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    // Indicates whether the game in paused mode
    bool pauseGame;

    // Show the pause menu in pause mode (the
    // first option will say "Resume")
    public void ShowPause()
    {
        // Pause the game
        pauseGame = true;
        // Show the panel
        gameObject.SetActive(true);
    }

    // Hide the menu panel
    public void Hide()
    {
        // Deactivate the panel
        gameObject.SetActive(false);
        // Resume the game (if paused)
        pauseGame = false;
        Time.timeScale = 1f;
    }

    public void Resume_Game() {
        Hide();
    }

    public void Restart_Game() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit_Game() {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {

        // If game is in pause mode, stop the timeScale value to 0
        if (pauseGame)
        {
            Time.timeScale = 0;
        }
    }
}