using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Watson.Anchors
{



    [CreateAssetMenu(fileName = "MarkerData", menuName = "Watson/MarkerData", order = 1)]
    public class MarkerData : ScriptableObject
    {
        public GameObject markerPrefab;
        public GameObject drawingPrefab;
        public GameObject notePrefab;
        public GameObject timeStampPrefab;
        [SerializeReference] public List<AnchorSave> anchorSaves = new List<AnchorSave>();

        public void SaveMarkerData(Marker marker)
        {
            Debug.Log("Saving marker data");
            if (marker.Type == MarkerType.Drawing) { SaveMarkerAsDrawing(marker); }
            else if (marker.Type == MarkerType.Note) { SaveMarkerAsNote(marker); }
            else if (marker.Type == MarkerType.TimeStamp) { SaveMarkerAsTimeStamp(marker); }
        }

        void SaveMarkerAsDrawing(Marker marker)
        {
            Drawing drawing = marker.GetComponentInChildren<Drawing>();
            if (drawing == null) return;

            DrawingData drawingData = new DrawingData();
            drawingData.points = drawing.GetDrawingData();
            drawingData.SetGuid(marker.Anchor.Uuid);

            anchorSaves.Add(drawingData);
        }

        void SaveMarkerAsNote(Marker marker)
        {
            Note note = marker.GetComponentInChildren<Note>();
            if (note == null) return;
            NoteData noteData = new NoteData();
            noteData.SetGuid(marker.Anchor.Uuid);
            noteData.text = note.noteText;

            anchorSaves.Add(noteData);
        }

        void SaveMarkerAsTimeStamp(Marker marker)
        {
            TimeStamp timeStamp = marker.GetComponentInChildren<TimeStamp>();
            if (timeStamp == null) return;

            TimeStampData timeStampData = new TimeStampData();
            timeStampData.SetGuid(marker.Anchor.Uuid);
            timeStampData.time = timeStamp.time;

            anchorSaves.Add(timeStampData);
        }

    }

    [System.Serializable]
    public class AnchorSave
    {
        [SerializeField] System.Guid anchorGuid = System.Guid.Empty;
        public System.Guid AnchorGuid => anchorGuid;
        public void SetGuid(System.Guid newGuid)
        {
            Debug.Log("Setting guid to " + newGuid);
            anchorGuid = newGuid;
            RefreshGuid();
        }
        public string guid = "";

        [Button]
        public void RefreshGuid()
        {
            guid = anchorGuid.ToString();
        }

        public AnchorSave()
        {
            guid = anchorGuid.ToString();
        }
    }

    public class NoteData : AnchorSave
    {
        public string text = "";
    }

    public class TimeStampData : AnchorSave
    {
        public System.DateTime time;
    }

    public class DrawingData : AnchorSave
    {
        public List<Vector3> points = new List<Vector3>();
    }
}
