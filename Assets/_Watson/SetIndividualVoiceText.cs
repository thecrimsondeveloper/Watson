using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Watson.Anchors
{
    public class SetIndividualVoiceText : MonoBehaviour
    {

        [SerializeField] GameObject defaultMarker;
        public float delay = 1.5f;
        public UnityEvent executeAfter;

        [Button]
        public void SetupNoteFromVoice()
        {
            StartCoroutine(GetTranslatedTextAfterDelay(delay));
        }

        IEnumerator GetTranslatedTextAfterDelay(float time)
        {
            Debug.Log("Debug: Waiting for " + time + " seconds");
            yield return new WaitForSeconds(time);

            Debug.Log("Debug: Getting last voice translation");

            LastVoiceTranslation voiceStringTranslation = GameObject.FindAnyObjectByType<LastVoiceTranslation>();
            string voiceString = "";

            if (voiceStringTranslation) voiceString = voiceStringTranslation.lastVoiceTranslation;
            else Debug.LogError("Debug Error: No LastVoiceTranslation found in scene");

            Debug.Log("Debug: -------- Last voice translation: " + voiceString);

            GameObject markerObj = Instantiate(defaultMarker, transform.position, Quaternion.identity);
            Marker marker = markerObj.GetComponent<Marker>();
            Debug.Log("Debug: Marker found: " + marker);
            if (marker)
            {
                marker.SetupNewNote(voiceString);
            }
            executeAfter.Invoke();
        }
    }

}
