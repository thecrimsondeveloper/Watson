using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Watson.Anchors
{
    public class TimeStamp : MonoBehaviour
    {
        public Marker parentMarker;
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
}
