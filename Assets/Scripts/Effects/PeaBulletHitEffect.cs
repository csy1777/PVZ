using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBulletHitEffect : MonoBehaviour
{
    private float hitEffectDestroyTimer=0;
    private float hitEffectDestroyTime=.15f;
    void Update()
    {
        hitEffectDestroyTimer+=Time.deltaTime;
        if (hitEffectDestroyTimer > hitEffectDestroyTime)
        {
            hitEffectDestroyTimer=0;
            PeaBulletHitPool.Instance.ReleasePrefab(gameObject);
        }
    }
}
