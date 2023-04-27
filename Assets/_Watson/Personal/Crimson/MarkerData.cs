using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;


namespace Watson.Anchors
{


    public class MarkerData : MonoBehaviour
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




            SaveMarkerData();
        }

        [Button]
        public void SaveMarkerData()
        {
            MarkerDataSave save = new MarkerDataSave(anchorSaves);
            string json = JsonUtility.ToJson(save);
            PlayerPrefs.SetString("MarkerData", json);
        }

        [Button]
        public void LoadMarkerData()
        {
            string jsonInput = PlayerPrefs.GetString("MarkerData", "");
            Debug.Log("Loading marker data : " + jsonInput);
            if (jsonInput == "") return;

            MarkerDataSave loadedSaves = JsonUtility.FromJson<MarkerDataSave>(jsonInput);

            Debug.Log("Loaded " + loadedSaves.GetCompiledAnchorSave().Count + " anchors");
            anchorSaves = loadedSaves.GetCompiledAnchorSave();
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
    public class MarkerDataSave
    {
        public List<NoteData> noteSave = new();
        public List<TimeStampData> timeStampSaves = new();
        public List<DrawingData> drawingSaves = new();


        public List<AnchorSave> GetCompiledAnchorSave()
        {
            List<AnchorSave> compiledSave = new List<AnchorSave>();
            compiledSave.AddRange(noteSave);
            compiledSave.AddRange(timeStampSaves);
            compiledSave.AddRange(drawingSaves);
            return compiledSave;
        }

        public MarkerDataSave() { }
        public MarkerDataSave(List<AnchorSave> markerSave)
        {
            foreach (AnchorSave save in markerSave)
            {
                if (save is DrawingData) { drawingSaves.Add((DrawingData)save); }
                else if (save is NoteData) { noteSave.Add((NoteData)save); }
                else if (save is TimeStampData) { timeStampSaves.Add((TimeStampData)save); }
            }
        }
    }

    [System.Serializable]
    public class AnchorSave
    {
        [SerializeField] System.Guid anchorGuid = System.Guid.Empty;
        public string guid = "";

        public System.Guid AnchorGuid => anchorGuid;
        public void SetGuid(System.Guid newGuid)
        {
            Debug.Log("Setting guid to " + newGuid);
            anchorGuid = newGuid;
            RefreshGuid();
        }

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

    [System.Serializable]
    public class NoteData : AnchorSave
    {
        public string text = "";
    }

    [System.Serializable]
    public class TimeStampData : AnchorSave
    {
        public System.DateTime time;
    }

    [System.Serializable]
    public class DrawingData : AnchorSave
    {
        public List<Vector3> points = new List<Vector3>();
    }
}
