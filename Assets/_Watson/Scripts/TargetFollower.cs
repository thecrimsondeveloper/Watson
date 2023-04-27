using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float speed = 1f;

    Vector3 posWithLocalOffset => target.position + target.TransformDirection(offset);

    private void OnValidate()
    {
        if (target != null) transform.position = posWithLocalOffset;
    }

    private void OnDrawGizmos()
    {
        if (target == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(posWithLocalOffset, 0.02f);
    }

    private void Update()
    {
        if (target == null) return;
        transform.position = Vector3.Lerp(transform.position, posWithLocalOffset, Time.deltaTime * speed);
    }

}
