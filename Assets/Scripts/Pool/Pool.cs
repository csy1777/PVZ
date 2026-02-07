using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Pool<T> : SingleTon<Pool<T>> where T:Pool<T> 
{
    public GameObject prefab;
    public ObjectPool<GameObject> pool;

    protected virtual void Awake()
    {
        base.Awake();
        pool=new ObjectPool<GameObject>(CreateFunc,ActionOnGet,ActionOnRelease,ActionOnDestroy,true,10,1000);
    }

    protected GameObject CreateFunc()
    {
        var obj=Instantiate(prefab);
        return obj;
    }
    protected void ActionOnGet(GameObject obj)
    {
        obj.SetActive(true);
    }
    protected void  ActionOnRelease(GameObject obj)
    {
        obj.SetActive(false);
    }

    protected void ActionOnDestroy(GameObject obj)
    {
        Destroy(obj);
    }

    public virtual GameObject GetPrefab(Vector3 pos)
    {
        GameObject go=pool.Get();
        go.transform.position=pos;
        return go;
    }

    public virtual void ReleasePrefab(GameObject obj)
    {
        pool.Release(obj);
        InitPrefab(obj);
    }

    public virtual void InitPrefab(GameObject obj)
    {
        
    }
}
