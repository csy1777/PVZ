using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : Plant
{
    public float shootDuration = 2;
    protected float shootTimer;
    protected Transform shootPos;
    public LayerMask targetLayer;
    public float watchDistance;
    public PeaBullet peaBullet;
    public float bulletSpeed;
    public int bulletAtkValue=20;
    protected override void Awake()
    {
        base.Awake();
        shootPos=transform.GetChild(0).transform;
        shootTimer = 0;
    }

    protected override void UpdateEnabled()
    {
        if (WatchZombie())
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootDuration)
            {
                shootTimer = 0;
                Shoot();
            }
        }
        else
        {
            shootTimer = 0;
        }
    }

    protected virtual void Shoot()
    {
        AudioManager.Instance.PlayClip(Config.shoot,1);
        //PeaBullet go=Instantiate(peaBullet,shootPos.position,Quaternion.identity);
        //go.SetBulletSpeed(bulletSpeed);
        //go.SetAtkValue(bulletAtkValue);
        
        //对象池代替PeaBullet的生成
        GameObject go=PeaBulletPool.Instance.GetPrefab(shootPos.position);
        PeaBullet peaBullet=go.GetComponent<PeaBullet>();
        peaBullet.SetBulletSpeed(bulletSpeed);
        peaBullet.SetAtkValue(bulletAtkValue);
    }

    private bool WatchZombie()
    {
        if (Physics2D.Raycast(shootPos.position,shootPos.right, watchDistance,targetLayer))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        if (shootPos != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(shootPos.position,shootPos.position+shootPos.right * watchDistance);
        }
    }
}
