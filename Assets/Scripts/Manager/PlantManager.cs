using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : SingleTon<PlantManager>
{
    private List<Plant> plants= new List<Plant>();

    public void DisablePlants()
    {
        foreach (Plant plant in plants)
        {
            plant.TransitionToDisable();
        }
    }

    public void EnablePlants()
    {
        foreach (Plant plant in plants)
        {
            plant.TransitionToEnable();
        }
    }
    public void AddPlant(Plant plant)
    {
        plants.Add(plant);
    }
    public void RemovePlant(Plant plant)
    {
        plants.Remove(plant);
    }
}
