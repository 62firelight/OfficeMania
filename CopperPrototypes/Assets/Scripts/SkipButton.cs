using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipButton : MonoBehaviour
{

    public float showTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (showTime > 0)
        {
            showTime -= Time.deltaTime;

            if (showTime <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        if (gameObject.activeInHierarchy == true && Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void SetShowTime()
    {
        showTime = 5f;
    }
}
