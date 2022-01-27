using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    public Transform holdspot;
    public LayerMask pickupMask;

    public Vector3 Direction { get; set; }
    private GameObject itemHolding;

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.F))
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
    }
}
