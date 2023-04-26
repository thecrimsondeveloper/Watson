using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class TimeSaver : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    [Button]
    public void SetTime()
    {
        text.text = System.DateTime.Now.ToString("HH:mm:ss");
    }
}
