using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sun : MonoBehaviour
{
    public float moveDuration;
    public float sunMoveDuration;
    public int sunPoint;
    public void JumpTo(Vector3 targetPos)
    {
        targetPos.z = -1;
        Vector3 ceterPos=(transform.position+targetPos)/2;
        float distance = Vector3.Distance(transform.position, targetPos);
        ceterPos.y+=(distance/2);
        transform.DOPath(new Vector3[] { transform.position, ceterPos, targetPos }, moveDuration, PathType.CatmullRom)
            .SetEase(Ease.OutQuad);
    }

    public void LinearTo(Vector3 targetPos)
    {
        transform.DOMove(targetPos, sunMoveDuration);
    }

    private void OnMouseDown()
    {
        transform.DOMove(SunManager.Instance.sunPointPos, moveDuration).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                SunManager.Instance.AddSunPoint(sunPoint);
                //Destroy(this.gameObject);
                
                //对象池代替阳光的回收
                SunPool.Instance.ReleasePrefab(gameObject);
            }
        );
    }
}
