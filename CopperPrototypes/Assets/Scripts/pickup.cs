using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    public Transform holdspot;
    public LayerMask pickupMask;

    public Vector3 Direction { get; set; }
    private GameObject itemHolding;
    private Vector3 endpoint;

    public GameObject destroyEffect;

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.P))
        {
            if (itemHolding)
            {
                itemHolding.transform.position = transform.position + Direction;
                itemHolding.transform.parent = null;
                if (itemHolding.GetComponent<Rigidbody2D>())
                    itemHolding.GetComponent<Rigidbody2D>().simulated = true;
                itemHolding = null;
            }
            else
            {
              Collider2D pickUpItem = Physics2D.OverlapCircle(transform.position + Direction, .4f, pickupMask);
                if (pickUpItem)
                {
                    itemHolding = pickUpItem.gameObject;
                    itemHolding.transform.position = holdspot.position;
                    itemHolding.transform.parent = transform;
                    if (itemHolding.GetComponent<Rigidbody2D>())
                        itemHolding.GetComponent<Rigidbody2D>().simulated = false; 
                        
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (itemHolding)
            {
                StartCoroutine(ThrowItem(itemHolding));
                itemHolding = null;
            }
        }
        IEnumerator ThrowItem(GameObject item)
        {
            Vector3 startPoint = item.transform.position + Direction * 2;
            item.transform.parent = null;
            for (int i = 0; i < 25; i++)
            {
                item.transform.position = Vector3.Lerp(startPoint, endpoint, i * .04f);
                yield return null;
            }
            if (item.GetComponent<Rigidbody2D>())
                item.GetComponent<Rigidbody2D>().simulated = true;
            Instantiate(destroyEffect, item.transform.position, Quaternion.identity);
            Destroy(item);
        }
    }
}
