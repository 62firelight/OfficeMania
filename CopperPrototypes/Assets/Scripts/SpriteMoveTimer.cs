using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMoveTimer : MonoBehaviour
{
    public float timeToMove = 0.0f;

    public float moveSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timeToMove > 0)
        {
            timeToMove -= Time.deltaTime;
        }
        else
        {
            transform.Translate(new Vector3(0, -moveSpeed * Time.deltaTime, 0));
        }
    }
}
