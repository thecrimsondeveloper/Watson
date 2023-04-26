using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Watson.Anchors
{


    [CreateAssetMenu(fileName = "MarkerData", menuName = "Watson/MarkerData", order = 1)]
    public class MarkerData : ScriptableObject
    {
        [SerializeReference] public List<AnchorSave> anchorSaves = new List<AnchorSave>();

        public void SaveMarkerData(Marker marker)
        {
            if (marker.Type == MarkerType.Drawing) { SaveMarkerAsDrawing(marker); }
            else if (marker.Type == MarkerType.Note) { SaveMarkerAsNote(marker); }
            else if (marker.Type == MarkerType.TimeStamp) { SaveMarkerAsTimeStamp(marker); }
        }

        void SaveMarkerAsDrawing(Marker marker)
        {
            LineRenderer drawing = marker.GetComponent<LineRenderer>();
            if (drawing == null) return;
            DrawingData drawingData = new DrawingData();
            drawingData.anchorGuid = marker.Anchor.Uuid;
            for (int i = 0; i < drawing.positionCount; i++)
            {
                drawingData.points.Add(drawing.GetPosition(i));
            }

            anchorSaves.Add(drawingData);
        }

        void SaveMarkerAsNote(Marker marker)
        {
            TMP_Text note = marker.GetComponentInChildren<TMP_Text>();
            if (note == null) return;
            NoteData noteData = new NoteData();
            noteData.anchorGuid = marker.Anchor.Uuid;
            noteData.text = note.text;

            anchorSaves.Add(noteData);
        }

        void SaveMarkerAsTimeStamp(Marker marker)
        {
            TMP_Text timeStamp = marker.GetComponentInChildren<TMP_Text>();
            if (timeStamp == null) return;
            TimeStamptData timeStampData = new TimeStamptData();
            timeStampData.anchorGuid = marker.Anchor.Uuid;
            timeStampData.time = timeStamp.text;

            anchorSaves.Add(timeStampData);
        }

    }

    [System.Serializable]
    public class AnchorSave
    {
        public System.Guid anchorGuid = System.Guid.Empty;
    }

    public class NoteData : AnchorSave
    {
        public string text = "";
    }

    public class TimeStamptData : AnchorSave
    {
        public string time = "";
    }

    public class DrawingData : AnchorSave
    {
        public List<Vector3> points = new List<Vector3>();
    }
}
