using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    public List<GameObject> objects;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.transform.parent != null && other.gameObject.transform.parent.tag == "Blunt") 
        {
            objects.Add(other.gameObject.transform.parent.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.transform.parent != null && other.gameObject.transform.parent.tag == "Blunt") 
        {
            objects.Remove(other.gameObject.transform.parent.gameObject);
        }
    }
}
