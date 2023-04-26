using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour
{
    [SerializeField] LineRenderer drawingLine;

    public void SetupDrawing(Vector3[] points)
    {
        drawingLine.useWorldSpace = false;
        drawingLine.positionCount = points.Length;
        drawingLine.SetPositions(points);
    }


    List<Vector3> tempPoints;
    public List<Vector3> GetDrawingData()
    {
        tempPoints.Clear();
        tempPoints = new List<Vector3>();
        for (int i = 0; i < drawingLine.positionCount; i++)
        {
            tempPoints.Add(drawingLine.GetPosition(i));
        }
        return tempPoints;
    }
}
