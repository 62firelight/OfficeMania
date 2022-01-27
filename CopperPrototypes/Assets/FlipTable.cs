using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipTable : MonoBehaviour
{
    public Sprite flippedSprite;

    private SpriteRenderer sr;

    private Collider2D uprightCollider;

    private bool isInteractable = false;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInParent<SpriteRenderer>();
        uprightCollider = transform.parent.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(uprightCollider);

        if (isInteractable)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                sr.sprite = flippedSprite;
                uprightCollider.enabled = false;

                Debug.Log("(┛◉Д◉)┛彡┻━┻");
            }
        }   
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        isInteractable = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isInteractable = false;
    }
}
