using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardBehaviour : MonoBehaviour
{

    private void OnValidate()
    {
        if (Camera.main) transform.LookAt(Camera.main.transform);
    }
    private void Update()
    {
        if (Camera.main) transform.LookAt(Camera.main.transform);
    }
}
