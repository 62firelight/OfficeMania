using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeycardDoor : MonoBehaviour
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
            PlayerKeycard playerKeycard = other.GetComponent<PlayerKeycard>();

            if (playerKeycard.hasKeycard)
            {
                Debug.Log("Access granted");
                transform.parent.gameObject.SetActive(false);

                if (tmp != null) tmp.text = "Access granted";
            }
            else
            {
                Debug.Log("Access denied");

                if (tmp != null) tmp.text = "Access denied";
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (tmp != null) tmp.text = string.Empty;
    }
}
