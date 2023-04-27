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
    public float delay =1.5f;
    public void ActivateVoiceSDK()
    {
        appDictation = GameObject.FindAnyObjectByType<AppDictationExperience>();
        appDictation.Activate();
    }

    IEnumerator GetTranslatedTextAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        text.text = GameObject.FindAnyObjectByType<LastVoiceTranslation>().lastVoiceTranslation;
    }

    public void GetTranslatedText()
    {
        StartCoroutine(GetTranslatedTextAfterDelay(delay));
    }
}
