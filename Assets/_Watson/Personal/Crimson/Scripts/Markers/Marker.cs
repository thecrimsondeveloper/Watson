using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Linq;
using Oculus.Interaction;

namespace Watson.Anchors
{

    public class Marker : MonoBehaviour
    {
        [SerializeField] MarkerManager manager;
        [SerializeField] OVRSpatialAnchor anchor;
        [SerializeField] PointableUnityEventWrapper pointerEventWrapper;
        public OVRSpatialAnchor Anchor => anchor;
        public string anchorGuid = "";
        public void SetAnchor(OVRSpatialAnchor newAnchor) => anchor = newAnchor;

        [SerializeField] MarkerType type;
        public MarkerType Type => type;
        public void SetType(MarkerType newType) => type = newType;


        private void OnValidate()
        {
            if (anchor == null) anchor = GetComponent<OVRSpatialAnchor>() ?? gameObject.AddComponent<OVRSpatialAnchor>();
            if (manager == null) manager = FindObjectOfType<MarkerManager>();
        }

        private void Start()
        {
            manager = FindObjectOfType<MarkerManager>();
            manager.AddMarker(this);

            anchor = GetComponent<OVRSpatialAnchor>();

            StartCoroutine(LateStart());
        }
        IEnumerator LateStart()
        {
            yield return new WaitForSeconds(1f);
            SetupMarker();
            Debug.Log("Debug: Marker Setup");
        }

        private void Update()
        {
            anchorGuid = anchor.Uuid.ToString();
        }


        public void Remove()
        {
            DeleteAnchor();
            Destroy(gameObject);
        }

        public void DeleteAnchor()
        {
            anchor.Erase();
            Destroy(gameObject);
        }

        [Button, HideInEditorMode]
        public void SetupMarker()
        {
            AnchorSave anchorSave = null;
            foreach (AnchorSave save in manager.data.anchorSaves)
            {
                Debug.Log($"Comparing Anchor: {anchor.Uuid} with " + save.guid);

                if (anchor.Uuid.ToString() == save.guid)
                {
                    anchorSave = save;
                    break;
                }
            }

            if (anchorSave == null)
            {
                Debug.LogError($"Debug No anchor save found for {anchor.Uuid}");
                return;
            }
            Debug.Log($"Debug: Setup Marker End {anchor.Uuid}");

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
                note.parentMarker = this;
                note.SetText(anchorSave.text);
            }
        }

        void SetupAsTimeStamp(TimeStampData anchorSave)
        {
            type = MarkerType.TimeStamp;
            SetupNewTimeStamp(anchorSave.time);
        }



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
                timeStamp.parentMarker = this;
                timeStamp.SetupTimeStamp();
            }

        }

        [Button, HideInEditorMode]
        public void SetupNewDrawing(Vector3[] points)
        {
            foreach (var point in points)
            {
                Debug.Log(point);
            }

            StartCoroutine(SetupNewDrawingRoutine(points));
        }

        IEnumerator SetupNewDrawingRoutine(Vector3[] points)
        {
            yield return new WaitForSeconds(0.1f);

            type = MarkerType.Drawing;

            GameObject drawingObj = Instantiate(manager.data.drawingPrefab, transform);
            drawingObj.transform.localPosition = Vector3.zero;

            Drawing drawing = drawingObj.GetComponent<Drawing>();
            if (drawing)
            {
                Debug.Log("Setting up drawing");
                drawing.parentMarker = this;
                yield return drawing.SetupDrawing(points);
            }
            else
            {
                Debug.LogError("No drawing component found");
            }
        }



        public const string NumUuidsPlayerPref = "numUuids";


        [Button, HideInEditorMode]
        public void SaveAnchor()
        {
            if (!anchor)
            {
                Debug.LogError("Debug: No anchor to save");
                return;
            }
            else
            {
                Debug.Log("Debug: Saving anchor");
            }

            Debug.Log("Debug: Try Saving anchor: " + anchor.Uuid.ToString());

            anchor.Save((anchor, success) =>
            {
                if (!success)
                {
                    Debug.LogError("Debug: Failed to save anchor");
                    return;
                }
                else Debug.Log("Deebug: Anchor saved successfully");

                // Write uuid of saved anchor to file
                if (!PlayerPrefs.HasKey(NumUuidsPlayerPref))
                {
                    PlayerPrefs.SetInt(NumUuidsPlayerPref, 0);
                }

                int playerNumUuids = PlayerPrefs.GetInt(NumUuidsPlayerPref);
                PlayerPrefs.SetString("uuid" + playerNumUuids, anchor.Uuid.ToString());
                PlayerPrefs.SetInt(NumUuidsPlayerPref, ++playerNumUuids);
                Debug.Log("Debug: Saving anchor: " + anchor.Uuid.ToString());
            });
        }
    }
}
