using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Watson.Anchors {
    public class SetIndividualVoiceText : MonoBehaviour
    {

        [SerializeField] GameObject defaultMarker;

        [Button]
        public void SetupNoteFromVoice()
        {
            string voiceString = GameObject.FindAnyObjectByType<LastVoiceTranslation>().lastVoiceTranslation;
            GameObject markerObj = Instantiate(defaultMarker, transform.position, Quaternion.identity);
            Marker marker = markerObj.GetComponent<Marker>();
                
            if (marker)
            {
                marker.SetupNewNote(voiceString);
            }
        }
    }

}
