using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropPickerItem : MonoBehaviour
{
    public GameObject markerPrefab;

    void RepopulateChild()
    {
        bool DoesTransformHaveChildren = transform.childCount > 0;



        if (DoesTransformHaveChildren)
        {
            // bool areChildrenActive = transform.GetChild(0).gameObject.activeSelf;
            // //destroy all children
            // if (areChildrenActive)
            // {


            //     return;
            // }
            // else
            // {
            //     for (int i = 0; i < transform.childCount; i++)
            //     {
            //         Destroy(transform.GetChild(i).gameObject);
            //     }
            // }
            return;
        }
        GameObject marker = Instantiate(markerPrefab, transform.position, transform.rotation);
        marker.transform.parent = transform;
        marker.transform.localScale = new Vector3(1, 1, 1);
    }

    void Update()
    {
        RepopulateChild();
    }
}
