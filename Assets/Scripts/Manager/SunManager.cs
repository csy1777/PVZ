using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Pool;

public class SunManager : SingleTon<SunManager>
{
    private TextMeshProUGUI sunPointText;
    //阳光数值的位置
    public Vector3 sunPointPos;
    public float produceTime=3f;
    public GameObject sunPrefab;
    private float produceTimer;
    private bool isStartProduce=false;
    public int SunPoint;
  protected void Awake()
  {
      base.Awake();
      sunPointText= GameObject.Find("SunPointText").GetComponent<TextMeshProUGUI>();
  }

  private void Start()
  {
      CalculateSunPosition();
  }

  public void StartProduceSun()
  {
      isStartProduce=true;
  }
  public void StopProduceSun()
  {
      isStartProduce=false;
  }


   private void Update()
   {
       UpdateSunPoint();
       if (isStartProduce)
       {
           ProduceSun();
       }
   }

   private void UpdateSunPoint()
   {
       sunPointText.text = SunPoint.ToString();
   }

   public void SubSunPoint(int sunPoint)
   {
       SunPoint -= sunPoint;
   }
   public void AddSunPoint(int sunPoint)
   {
       SunPoint += sunPoint;
   }
   

   private void CalculateSunPosition()
   {
       //计算阳光数值的世界位置
       /*Vector3 position=Camera.main.ScreenToWorldPoint(sunPointText.transform.position);
       position.z=0;
       sunPointPos=position;*/
       sunPointPos=sunPointText.rectTransform.position+new Vector3(0,-.5f,0);
   }

   private void ProduceSun()
   {
       produceTimer+=Time.deltaTime;
       if (produceTimer >= produceTime)
       {
           produceTimer=0;
           Vector3 position=new Vector3(Random.Range(-5, 5),6f,-1);
           //GameObject go=Instantiate(sunPrefab,position,Quaternion.identity);
           
           //对象池代替SunManager的阳光的复用
           GameObject go = SunPool.Instance.GetPrefab(position);
           position.y=Random.Range(-4,3);
           go.GetComponent<Sun>().LinearTo(position);
       }
   }
}
