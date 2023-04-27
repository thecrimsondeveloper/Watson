using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class ScaleInOut : MonoBehaviour
{
    Vector3 startScale;
    [SerializeField] bool isHiddenOnStart = true;
    private void Start()
    {
        startScale = transform.localScale;
        if (isHiddenOnStart) transform.localScale = Vector3.zero;
    }

    [Button]
    public void ScaleOut()
    {
        //kill any previous tweens
        DOTween.Kill(transform);
        transform.DOScale(0, 0.5f).SetEase(Ease.InBack);
    }

    [Button]
    public void ScaleIn()
    {
        //kill any previous tweens
        DOTween.Kill(transform);
        transform.DOScale(startScale, 0.5f).SetEase(Ease.OutBounce);
    }

    [Button]
    public void ScaleOutDestroy()
    {
        //kill any previous tweens
        DOTween.Kill(transform);
        transform.DOScale(0, 0.5f).SetEase(Ease.InBack).OnComplete(() => Destroy(gameObject));
    }
}
