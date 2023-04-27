using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnableAfterTime : MonoBehaviour
{
    [SerializeField] List<Behaviour> behaviours = new List<Behaviour>();

    [Button]
    public void EnableAfterTimePassed(float time)
    {
        StartCoroutine(EnableAfterTimeCoroutine(time));
    }

    IEnumerator EnableAfterTimeCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        foreach (var behaviour in behaviours)
        {
            behaviour.enabled = true;
        }
    }


}
