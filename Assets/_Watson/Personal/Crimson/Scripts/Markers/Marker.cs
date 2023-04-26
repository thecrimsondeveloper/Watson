using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Linq;

namespace Watson.Anchors
{

    [RequireComponent(typeof(OVRSpatialAnchor))]
    public class Marker : MonoBehaviour
    {
        [SerializeField] MarkerManager manager;
        [SerializeField] OVRSpatialAnchor anchor;
        public OVRSpatialAnchor Anchor => anchor;
        public void SetAnchor(OVRSpatialAnchor newAnchor) => anchor = newAnchor;

        [SerializeField] MarkerType type;
        public MarkerType Type => type;
        public void SetType(MarkerType newType) => type = newType;


        private void Start()
        {
            manager = FindObjectOfType<MarkerManager>();
            manager.AddMarker(this);

            anchor = GetComponent<OVRSpatialAnchor>();

            // SetupMarker();
        }



        public void Remove()
        {
            DeleteAnchor();
            Destroy(gameObject);
        }

        public void Save()
        {
            anchor.Save();
        }
        public void DeleteAnchor()
        {
            anchor.Erase();
        }

        [Button]
        public void SetupMarker()
        {
            System.Guid testGuid = manager.data.anchorSaves[0].anchorGuid;
            AnchorSave anchorSave = manager.data.anchorSaves.Find(a => a.anchorGuid == anchor.Uuid);
            if (anchorSave == null) return;

            if (anchorSave is DrawingData) { SetupAsDrawing((DrawingData)anchorSave); }
            else if (anchorSave is NoteData) { SetupAsNote((NoteData)anchorSave); }
            else if (anchorSave is TimeStampData) { SetupAsTimeStamp((TimeStampData)anchorSave); }

        }

        void SetupAsDrawing(DrawingData anchorSave)
        {
            type = MarkerType.Drawing;
            Vector3[] points = anchorSave.points.ToArray();
            SetupNewDrawing(points);
        }

        void SetupAsNote(NoteData anchorSave)
        {
            type = MarkerType.Note;

            GameObject noteObj = Instantiate(manager.data.notePrefab, transform);
            noteObj.transform.localPosition = Vector3.zero;

            Note note = noteObj.GetComponent<Note>();
            if (note)
            {
                note.SetText(anchorSave.text);
            }
        }

        void SetupAsTimeStamp(TimeStampData anchorSave)
        {
            type = MarkerType.TimeStamp;
            SetupNewTimeStamp(anchorSave.time);
        }


        [Button]
        public void SetupNewTimeStamp(System.DateTime time)
        {
            StartCoroutine(SetupNewTimeStampRoutine(time));
        }

        IEnumerator SetupNewTimeStampRoutine(System.DateTime time)
        {
            yield return new WaitForSeconds(0.5f);

            type = MarkerType.TimeStamp;

            GameObject timeStampObj = Instantiate(manager.data.timeStampPrefab, transform);
            timeStampObj.transform.localPosition = Vector3.zero;

            TimeStamp timeStamp = timeStampObj.GetComponent<TimeStamp>();
            if (timeStamp)
            {
                timeStamp.SetupTimeStamp();
            }

        }

        [Button]
        public void SetupNewDrawing(Vector3[] points)
        {
            StartCoroutine(SetupNewDrawingRoutine(points));
        }

        IEnumerator SetupNewDrawingRoutine(Vector3[] points)
        {
            yield return new WaitForSeconds(0.5f);

            type = MarkerType.Drawing;

            GameObject drawingObj = Instantiate(manager.data.drawingPrefab, transform);
            drawingObj.transform.localPosition = Vector3.zero;

            Drawing drawing = drawingObj.GetComponent<Drawing>();
            if (drawing)
            {
                drawing.SetupDrawing(points);
            }
        }


    }
}
