using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        if (target != null)
        {
            transform.position = target.transform.position;

            transform.Translate(0, 1.5f, 0);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
