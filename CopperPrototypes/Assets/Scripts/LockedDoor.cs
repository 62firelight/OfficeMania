using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockedDoor : MonoBehaviour
{

    private SpriteRenderer sr;

    private TextMeshPro tmp;

    // Start is called before the first frame update
    void Start()
    {
        tmp = transform.parent.GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (tmp != null) tmp.text = "Inaccessible";
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (tmp != null) tmp.text = string.Empty;
    }
}
