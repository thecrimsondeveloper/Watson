using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SaveLogic : MonoBehaviour
{
    public UnityEvent save_Event = new UnityEvent();

    void Start()
    {
        //save_Event.AddListener(Ping);
    }

    void callExample()
    {
        save_Event.Invoke();
    }

    public void SaveCrimeScene()
    {
        // Save Anchors Here

    }
}
