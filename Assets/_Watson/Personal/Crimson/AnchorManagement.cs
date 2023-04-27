using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AnchorManagement : MonoBehaviour
{

    [Button]
    public void EraseAllSpatialAnchors()
    {
        //get all anchors
        var allAnchors = FindObjectsOfType<OVRSpatialAnchor>();

        Debug.Log("Debug: Found " + allAnchors.Length + " anchors to erase");

        //destroy all anchors
        foreach (var anchor in allAnchors)
        {
            anchor.Erase();
            Destroy(anchor.gameObject);
        }

    }
}
