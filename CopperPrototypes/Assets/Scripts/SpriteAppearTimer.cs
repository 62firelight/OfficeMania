using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAppearTimer : MonoBehaviour
{

    public float timeToAppear = 0.0f;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToAppear > 0)
        {
            timeToAppear -= Time.deltaTime;

            if (timeToAppear <= 0)
            {
                sr.enabled = true;
            }
        }
    }
}
