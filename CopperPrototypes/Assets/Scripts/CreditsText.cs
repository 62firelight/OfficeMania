using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > Camera.main.transform.position.y && !Utility.isVisible(GetComponent<Renderer>(), Camera.main))
        {
            Destroy(gameObject);
        }
    }
}
