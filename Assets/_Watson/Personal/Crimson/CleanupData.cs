using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CleanupData : MonoBehaviour
{
    public UnityEvent cleanupEvent;
    public void Cleanup()
    {
        Debug.Log("Debug: Cleaning up " + gameObject.name);
        cleanupEvent.Invoke();
        PlayerPrefs.DeleteKey("MarkerData");




    }
}
