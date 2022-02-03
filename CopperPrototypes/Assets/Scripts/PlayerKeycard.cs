using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeycard : MonoBehaviour
{
    public bool hasKeycard = false;

    public void SetHasKeycard(bool status)
    {
        hasKeycard = status;
    }
}
