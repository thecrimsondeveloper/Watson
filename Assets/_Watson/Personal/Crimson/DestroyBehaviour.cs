using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBehaviour : MonoBehaviour
{

    public void Destroy(float time = 0)
    {
        Destroy(gameObject, time);
    }


}
