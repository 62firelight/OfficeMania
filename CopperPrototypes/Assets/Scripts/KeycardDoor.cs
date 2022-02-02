using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardDoor : MonoBehaviour
{

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerKeycard playerKeycard = other.GetComponent<PlayerKeycard>();

            if (playerKeycard.hasKeycard)
            {
                Debug.Log("Access granted");
                transform.parent.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Access denied");
            }
        }
    }
}
