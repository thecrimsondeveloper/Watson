using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class TimeStamp : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    public System.DateTime time;

    [Button]
    public void SetTime()
    {
        text.text = time.ToString("HH:mm:ss");
    }

    public void SetupTimeStamp(System.DateTime? time = null)
    {
        if (time == null) time = System.DateTime.Now;
        this.time = time.Value;
        SetTime();
    }
}
