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
                Debug.Log("Unlocked");
                transform.parent.gameObject.SetActive(false);

                if (tmp != null) tmp.text = "Unlocked";
            }
            else
            {
                Debug.Log("Key required");

                if (tmp != null) tmp.text = "Key required";
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (tmp != null) tmp.text = string.Empty;
    }
}
