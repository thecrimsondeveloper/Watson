using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardBehaviour : MonoBehaviour
{

    private void OnValidate()
    {
        transform.LookAt(Camera.main.transform);
    }
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
