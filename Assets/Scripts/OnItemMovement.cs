using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnItemMovement : MonoBehaviour
{
    Transform myOrgTransform;
    // Start is called before the first frame update
    void Start()
    {
        myOrgTransform = transform;
    }

    public Transform GetOriginalTransform()
    {
        return myOrgTransform;
    }

    public Vector3 GetScale()
    {
        return transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
