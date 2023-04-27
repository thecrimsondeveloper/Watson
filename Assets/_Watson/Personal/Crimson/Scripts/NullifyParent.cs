using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullifyParent : MonoBehaviour
{
    public void Nullify(bool resetScale)
    {
        transform.SetParent(null);
        if (resetScale)
        {
            transform.localScale = Vector3.one;
        }
    }
}
