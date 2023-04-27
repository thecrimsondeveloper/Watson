using Oculus.Voice.Dictation;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivateVoice : MonoBehaviour
{
    private AppDictationExperience appDictation;
    public TextMeshProUGUI text;
    public void ActivateVoiceSDK()
    {
        appDictation = GameObject.FindAnyObjectByType<AppDictationExperience>();
        appDictation.Activate();
    }

    public void GetTranslatedText()
    {
        text.text = GameObject.FindAnyObjectByType<LastVoiceTranslation>().lastVoiceTranslation;
    }
}
