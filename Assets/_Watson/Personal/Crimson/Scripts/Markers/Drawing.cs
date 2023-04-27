using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Watson.Anchors
{
    public class Drawing : MonoBehaviour
    {
        public Marker parentMarker;
        [SerializeField] LineRenderer drawingLine;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.02f);
        }

        public IEnumerator SetupDrawing(Vector3[] points)
        {

            foreach (var point in points)
            {
                Debug.Log("Point: " + point);
            }
            yield return new WaitForEndOfFrame();
            drawingLine.useWorldSpace = false;
            Debug.Log("Drawing line: " + points.Length);
            drawingLine.positionCount = points.Length;


            // for (int i = 0; i < points.Length; i++)
            // {
            //     Vector3 inversePoint = transform.InverseTransformPoint(points[i]);
            //     points[i] = transform.InverseTransformPoint(inversePoint);
            // }


            for (int i = 0; i < points.Length; i++)
            {
                Debug.Log("Point " + i + ": " + points[i]);
            }

            drawingLine.SetPositions(points);
        }




        List<Vector3> tempPoints = new List<Vector3>();
        public List<Vector3> GetDrawingData()
        {
            tempPoints.Clear();
            tempPoints = new List<Vector3>();
            for (int i = 0; i < drawingLine.positionCount; i++)
            {
                tempPoints.Add(drawingLine.GetPosition(i));
            }

            Debug.Log("Drawing data: " + tempPoints.Count);
            return tempPoints;
        }
    }
}
