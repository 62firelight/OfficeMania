using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelName : MonoBehaviour
{

    public float displayTime = 5f;

    private bool startFading = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (displayTime > 0)
        {
            displayTime -= Time.deltaTime;

            if (displayTime <= 0)
            {
                startFading = true;
                // gameObject.SetActive(false);
            }
        }

        if (startFading == true)
        {
            SpriteRenderer[] children = GetComponentsInChildren<SpriteRenderer>();
            Color newColor;

            bool finishedFading = true;

            foreach (SpriteRenderer child in children) {
                newColor = child.color;
                newColor.a -= 0.5f * Time.deltaTime;

                if (newColor.a > 0)
                {
                    finishedFading = false;
                }
                else
                {
                    gameObject.SetActive(false);
                    startFading = false;
                    break;
                }

                child.color = newColor;
            }
        }
    }
}
