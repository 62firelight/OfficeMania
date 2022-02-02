using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipTable : MonoBehaviour
{
    public Sprite flippedSprite;

    private bool flipped = false;

    private SpriteRenderer sr;

    private Collider2D uprightCollider;

    private Collider2D flippedCollider;

    private Rigidbody2D rb;

    private GameObject leftLeg;

    private GameObject rightLeg;

    private bool isInteractable = false;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInParent<SpriteRenderer>();
        uprightCollider = transform.parent.GetComponent<Collider2D>();
        flippedCollider = transform.parent.GetChild(2).gameObject.GetComponent<Collider2D>();
        rb = GetComponentInParent<Rigidbody2D>();

        leftLeg = transform.parent.GetChild(0).gameObject;
        rightLeg = transform.parent.GetChild(1).gameObject;

        leftLeg.SetActive(false);
        rightLeg.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (flipped == false && isInteractable)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                flipped = true;

                sr.sprite = flippedSprite;
                uprightCollider.enabled = false;
                leftLeg.SetActive(true);
                rightLeg.SetActive(true);

                rb.MovePosition(rb.position + new Vector2(0, 0.45f));

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
