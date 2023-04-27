using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class DebugUI : MonoBehaviour
{
    [SerializeField] GameObject debugTextPrefab;
    [SerializeField] Transform debugTextParent;

    private void Start()
    {
        //on application console 
        Application.logMessageReceived += HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {

        if (type == LogType.Log)
        {
            if (logString.Contains("Debug") == false) return;
            GameObject debugText = Instantiate(debugTextPrefab, debugTextParent);
            TMP_Text text = debugText.GetComponent<TMP_Text>();
            if (text)
            {
                text.text = logString;
            }
        }
        else if (type == LogType.Error)
        {
            if (logString.Contains("Debug") == false) return;
            GameObject debugText = Instantiate(debugTextPrefab, debugTextParent);
            TMP_Text text = debugText.GetComponent<TMP_Text>();
            if (text)
            {
                text.text = logString;
                text.color = Color.red;
            }
        }


    }


    [Button]
    void TestDebug()
    {
        Debug.Log("Debug: Test Debug");
    }

    [Button]
    void TestError()
    {
        Debug.LogError("Debug: Test Error");
    }
}
