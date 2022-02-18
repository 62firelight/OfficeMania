using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTutorial : MonoBehaviour
{
    public Interactable firstObject;

    private SpriteRenderer[] srList;

    // Start is called before the first frame update
    void Start()
    {
        srList = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sr in srList)
        {
            sr.color -= new Color (0, 0, 0, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (firstObject.pickedUp == true)
        {
            foreach (SpriteRenderer sr in srList)
            {
                sr.color += new Color (0, 0, 0, 1f);
            }
        }
    }
}
