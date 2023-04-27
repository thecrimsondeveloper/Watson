using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Watson.Anchors {
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
            yield return new WaitForSeconds(time);
            string voiceString = GameObject.FindAnyObjectByType<LastVoiceTranslation>().lastVoiceTranslation; GameObject markerObj = Instantiate(defaultMarker, transform.position, Quaternion.identity);
            Marker marker = markerObj.GetComponent<Marker>();

            if (marker)
            {
                marker.SetupNewNote(voiceString);
            }
            executeAfter.Invoke();
        }
    }

}
