using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : SingleTon<HandManager>
{
    //可以种植的植物
    public List<Plant> plants = new List<Plant>();
    private Plant currentPlant;

    private void Update()
    {
        FollowCurSor();
    }

    public bool AddPlant(PlantType plantType)
    {
        //当前手上的植物为空时,在点击卡片后将植物赋值给当前植物并实例化
        if (currentPlant != null) return false;
        Plant plant = GetPlant(plantType);
        if (plant == null)
        {
            Debug.Log("没找到该植物");
            return false;
        }
        currentPlant = GameObject.Instantiate(plant);
        return true;
        
    }
    private Plant GetPlant(PlantType plantType)
    {
        foreach(Plant plant in plants)
        {
            if (plant.plantType == plantType)
            {
                return plant;
            }
        }
        return null;
    }

    public void FollowCurSor()
    {
        if (currentPlant == null)
        {
            return;
        }
        Vector3 currentPlantPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPlantPos.z=0;
        currentPlant.transform.position = currentPlantPos;
    }

    public void OnCellClick(Cell cell)
    {
        if(currentPlant==null)return ;
        bool isSucess=cell.AddPlant(currentPlant);
        if (isSucess)
        {
            AudioManager.Instance.PlayClip(Config.plant,1);
            PlantManager.Instance.AddPlant(currentPlant);
            currentPlant = null;
        }
    }
}
