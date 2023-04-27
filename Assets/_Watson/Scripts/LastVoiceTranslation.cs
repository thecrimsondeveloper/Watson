using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastVoiceTranslation : MonoBehaviour
{
    public string lastVoiceTranslation = "";

    public void UpdateLastVoiceTranslation(string text)
    {
        lastVoiceTranslation = text; 
    }
}
