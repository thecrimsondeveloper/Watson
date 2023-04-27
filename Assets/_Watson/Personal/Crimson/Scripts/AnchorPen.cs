using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


namespace Watson.Anchors
{

    [RequireComponent(typeof(LineRenderer))]
    public class AnchorPen : MonoBehaviour
    {
        [SerializeField] LineRenderer drawing;
        public LineRenderer Drawing => drawing;
        [SerializeField, Range(0.01f, 0.1f)] float minVertexDistance = 0.1f;
        [SerializeField, Range(0, 1)] float lineSmoothing = 0.1f;
        [SerializeField] Vector3 targetDrawPoint = Vector3.zero;
        [SerializeField] GameObject defaultMarkerPrefab;
        bool isDrawing = false;

        private void OnDrawGizmos()
        {
            if (isDrawing)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(targetDrawPoint, 0.03f);
            }
        }

        private void OnValidate()
        {
            if (drawing == null) drawing = GetComponent<LineRenderer>() ?? gameObject.AddComponent<LineRenderer>();
        }


        private void Update()
        {

            if (!isDrawing) return;

            transform.rotation = Quaternion.identity;
            targetDrawPoint = Vector3.Lerp(targetDrawPoint, transform.position, Time.deltaTime * 10 * 1 / (lineSmoothing + 0.01f));

            if (Vector3.Distance(drawing.GetPosition(drawing.positionCount - 1), targetDrawPoint) > minVertexDistance)
            {
                drawing.positionCount++;
                drawing.SetPosition(drawing.positionCount - 1, targetDrawPoint);
            }
        }


        [Button]
        public void ToggleDrawing()
        {
            if (isDrawing)
            {
                isDrawing = false;
                SaveAndReset();
            }
            else
            {
                targetDrawPoint = transform.position;
                isDrawing = true;
                drawing.positionCount = 1;
                drawing.SetPosition(0, targetDrawPoint);
            }
        }

        public void SaveAndReset()
        {
            isDrawing = false;
            GameObject markerObj = Instantiate(defaultMarkerPrefab, transform.position, Quaternion.identity);
            transform.rotation = Quaternion.identity;

            List<Vector3> positionsInLocalSpace = new List<Vector3>();
            for (int i = 0; i < drawing.positionCount; i++)
            {
                Vector3 localPos = transform.InverseTransformPoint(drawing.GetPosition(i));
                Debug.Log("Local pos: " + localPos);
                positionsInLocalSpace.Add(localPos);
                Debug.Log("Local pos: " + positionsInLocalSpace[i]);
            }

            Marker marker = markerObj.GetComponent<Marker>();
            if (marker)
            {
                marker.SetupNewDrawing(positionsInLocalSpace.ToArray());
            }

            drawing.positionCount = 0;
        }
    }
}
