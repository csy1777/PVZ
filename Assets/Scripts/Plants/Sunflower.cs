using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sunflower : Plant
{
    public float produceDuration = 2;
    public GameObject sunPrefab;
    private float produceTimer;
    public float JumpMinDistance = 1;
    public float JumpMaxDistance = 2;
    protected void Awake()
    {
        base.Awake();
    }

    protected  void Start()
    {
        base.Start();
    }

    protected  override void Update()
    {
        base.Update();
    }
    protected override void UpdateEnabled()
    {
        produceTimer+= Time.deltaTime;
        if (produceTimer >= produceDuration)
        {
            produceTimer = 0;
            anim.SetTrigger("isGlowing");
        }
    }
    public void ProduceSun()
    {
        //GameObject go=Instantiate(sunPrefab, transform.position, Quaternion.identity);
        
        //对象池代替Sunflower的阳光的复用
        GameObject go = SunPool.Instance.GetPrefab(transform.position); 
        if (go != null)
        {
            /*float distance = Random.Range(JumpMinDistance, JumpMaxDistance);
            distance = Random.Range(0, 2) < 1 ? distance : -distance;
            Vector3 position = transform.position;
            position.x += distance;*/
            Vector3 position = CaculateRandomPosition();
            go.GetComponent<Sun>().JumpTo(position);
        }
    }

    private Vector3 CaculateRandomPosition()
    {
        float distance = Random.Range(JumpMinDistance, JumpMaxDistance);
        distance = Random.Range(0, 2) < 1 ? distance : -distance;
        Vector3 position = transform.position;
        position.x += distance;
        return position;
    }
}
