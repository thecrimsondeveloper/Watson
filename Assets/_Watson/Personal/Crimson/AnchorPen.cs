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

        [SerializeField, Range(0.01f, 0.1f)] float minVertexDistance = 0.1f;
        bool isDrawing = false;


        private void OnValidate()
        {

            if (drawing == null) drawing = GetComponent<LineRenderer>() ?? gameObject.AddComponent<LineRenderer>();

        }

        private void Update()
        {
            if (!isDrawing) return;

            if (Vector3.Distance(drawing.GetPosition(drawing.positionCount - 1), transform.position) > minVertexDistance)
            {
                drawing.positionCount++;
                drawing.SetPosition(drawing.positionCount - 1, transform.position);
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
                isDrawing = true;
                drawing.positionCount = 1;
                drawing.SetPosition(0, transform.position);
            }
        }

        public void SaveAndReset()
        {
            isDrawing = false;
            Instantiate(gameObject, drawing.transform.parent);
            drawing.positionCount = 0;
        }
    }
}
