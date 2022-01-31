using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuChecker : MonoBehaviour
{

    public GameObject pauseMenuPanel;
    PauseMenu pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = pauseMenuPanel.GetComponent<PauseMenu>();
        pauseMenu.Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            pauseMenu.ShowPause();
        }
    }
}
