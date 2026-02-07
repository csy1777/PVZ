using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet : MonoBehaviour
{
    private float speed = 2;
    private int atkValue;
    private float bulletDestroyTimer=0;
    private float bulletDestroyTime=5f;
    
    public GameObject peaBulletHitEffect;

    private void Start()
    {
        //Destroy(gameObject, 5f);
    }
    

    public void SetAtkValue(int atkValue)
    {
        this.atkValue = atkValue;
    }

    public void SetBulletSpeed(float speed)
    {
        this.speed = speed;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime));
        
        
        //对象池代替没碰到敌人的子弹的回收
        bulletDestroyTimer += Time.deltaTime;
        if (bulletDestroyTimer > bulletDestroyTime)
        {
            bulletDestroyTimer=0;
            PeaBulletPool.Instance.ReleasePrefab(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Zombie")
        {
            other.GetComponent<Zombie>().TakeDamage(atkValue);
            //Destroy(gameObject); 
            
            //对象池代替碰到敌人的子弹的回收
            PeaBulletPool.Instance.ReleasePrefab(gameObject);
            bulletDestroyTimer=0;
            
            //GameObject go=Instantiate(peaBulletHitEffect, transform.position, Quaternion.identity);
            //Destroy(go, .15f);
            
            //对象池代替子弹打中特效的生成
            PeaBulletHitPool.Instance.GetPrefab(transform.position);
            
        }
    }
}
