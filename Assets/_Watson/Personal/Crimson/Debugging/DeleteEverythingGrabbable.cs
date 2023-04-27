using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteEverythingGrabbable : MonoBehaviour
{
    public void DeleteEverything()
    {
        //find objects of type cleanup
        CleanupData[] cleanupObjects = FindObjectsOfType<CleanupData>();
        //call cleanup on them

        Debug.Log("Debug: Found " + cleanupObjects.Length + " cleanup objects");
        foreach (CleanupData cleanup in cleanupObjects)
        {
            Debug.Log("Debug: Calling cleanup on " + cleanup.gameObject.name);
            cleanup.Cleanup();
        }
    }
}
