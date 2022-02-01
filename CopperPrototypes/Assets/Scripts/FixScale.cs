using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(ScaleConstraint))]
public class FixScale : MonoBehaviour
{
    Quaternion rotation;

    GameObject scaleConstraint;

    public GameObject defaultScaleConstraint;

    ScaleConstraint sc;

    void Awake()
    {
        Transform parent = transform.parent;

        transform.SetParent(null);
        transform.localScale = new Vector3(1, 1, 1);
        transform.SetParent(parent);
        transform.localPosition = parent.localPosition;

        sc = GetComponent<ScaleConstraint>();

        scaleConstraint = GameObject.FindGameObjectWithTag("RotationConstraint");
        if (scaleConstraint == null)
        {
            scaleConstraint = Instantiate(defaultScaleConstraint, Vector3.zero, Quaternion.identity);
            scaleConstraint.tag = "RotationConstraint";
        }
    
        ConstraintSource source = new ConstraintSource();
        source.sourceTransform = scaleConstraint.transform;
        source.weight = 1;

        List<ConstraintSource> sources = new List<ConstraintSource>();
        sources.Add(source);

        sc.SetSources(sources);

        sc.constraintActive = true;
        sc.enabled = true;
    }

    void Update()
    {
        Debug.Log("localScale: " + transform.localScale);
    }
}
