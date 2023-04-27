using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Watson.Anchors
{
    public class Note : MonoBehaviour
    {
        public Marker parentMarker;
        public TMP_Text text;
        public string noteText => text.text;
        public void SetText(string newText) => text.text = newText;
    }
}
