using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
   Plant currentPlant;

   private void OnMouseDown()
   {
      //当手上的植物点击草地时把当前植物种植在当前Cell
      HandManager.Instance.OnCellClick(this);
   }

   public bool AddPlant(Plant plant)
   {
      //添加植物时先判断当前位置的植物是否为空,若已经有植物了不能种植,种植后设置植物位置并设置植物可用状态
      if(currentPlant!=null)return false;
      currentPlant=plant;
      currentPlant.transform.position = transform.position;
      plant.TransitionToEnable();
      return true;
   }
}
