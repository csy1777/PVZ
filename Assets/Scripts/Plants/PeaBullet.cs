using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet : MonoBehaviour
{
    private float speed = 2;
    private int atkValue;
    private float DestroyTimer=0;
    private float DestroyTime=5f;
    
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
        DestroyTimer += Time.deltaTime;
        if (DestroyTimer > DestroyTime)
        {
            DestroyTimer=0;
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
            DestroyTimer=0;
            GameObject go=Instantiate(peaBulletHitEffect, transform.position, Quaternion.identity);
            Destroy(go, .15f);
        }
    }
}
