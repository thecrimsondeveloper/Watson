using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FacingCameraEvents : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float maxAllowedDotProduct = 0.5f;
    [SerializeField] Vector3 directionToTarget;
    [SerializeField] float dotForForward;

    public UnityEvent OnLookEnter;
    public UnityEvent OnLookExit;
    Vector3 lookDirection => transform.up;

    private void OnDrawGizmos()
    {
        if (Application.isPlaying == false) { SetData(); }
        if (target)
        {
            Gizmos.color = dotForForward > maxAllowedDotProduct ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position, target.position);
            Gizmos.DrawRay(transform.position, lookDirection);
        }
    }

    bool inZoneLastFrame = false;
    private void Update()
    {
        SetData();
        bool isInZone = dotForForward > maxAllowedDotProduct;

        if (isInZone && inZoneLastFrame == false)
        {
            OnEnterZone();
        }
        else if (isInZone == false && inZoneLastFrame)
        {
            OnExitZone();
        }

        inZoneLastFrame = isInZone;
    }

    void SetData()
    {
        if (target == null) return;
        directionToTarget = (target.position - transform.position).normalized;
        dotForForward = Vector3.Dot(lookDirection, directionToTarget);
    }

    void OnEnterZone()
    {
        OnLookEnter.Invoke();
        Debug.Log("OnEnterZone");
    }

    void OnExitZone()
    {
        OnLookExit.Invoke();
        Debug.Log("OnExitZone");
    }
}
