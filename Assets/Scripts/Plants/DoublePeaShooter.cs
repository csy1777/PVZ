using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePeaShooter :PeaShooter
{
    protected override void Shoot()
    {
        base.Shoot();
        StartCoroutine(DoubleShoot());
    }
    IEnumerator  DoubleShoot()
    {
        yield return new WaitForSeconds(.15f);
        GameObject go=PeaBulletPool.Instance.GetPrefab(shootPos.position);
        PeaBullet peaBullet=go.GetComponent<PeaBullet>();
        peaBullet.SetBulletSpeed(bulletSpeed);
        peaBullet.SetAtkValue(bulletAtkValue);
    }
}
