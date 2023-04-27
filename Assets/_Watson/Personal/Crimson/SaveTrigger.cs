using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watson.Anchors;

public class SaveTrigger : MonoBehaviour
{

    //find the manager and save
    public void TrySave()
    {
        MarkerManager man = FindAnyObjectByType<MarkerManager>();
        if (man)
        {
            man.SaveAll();
        }
    }
}
