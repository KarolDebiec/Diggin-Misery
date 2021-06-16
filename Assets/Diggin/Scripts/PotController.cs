using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotController : MonoBehaviour
{
    public GameObject plant;
    private Vector3 plantPos;
    public GameObject plantDummy; // when there are no growing plant this is taking their place
    public Seeds seedPlanted;
    public float progress; // 0-100
    public float growthSpeed; 
    public float takingWaterSpeed; // speed at which water dissapears
    public float water;     // water in plant if lless than minimum over duration plant dies
    private float waterMin;
    public float timeToWither;  // set time until plant wither
    public float witherCount; // real time until plant wither
    private bool readyToHarvest;
    private bool canPlant=true;
    private bool withered;
    public int lastChange=0;
    public int activePhase=1;
    private int phases=1;

    public bool test;
    void Update()
    {
        if(!readyToHarvest)
        {
            witherCount = timeToWither;
            progress += growthSpeed * Time.deltaTime;
        }
        else
        {
            witherCount -= takingWaterSpeed * Time.deltaTime;
            if (witherCount < 0)
            {
                Wither();
            }
        }
        if(progress>=100 && !readyToHarvest)
        {
            readyToHarvest = true;
            ChangePlantObject(seedPlanted.growthSteps[phases+1]);
        }
        if(water > 0)
        {
            water -= takingWaterSpeed * Time.deltaTime;
            witherCount = timeToWither;
        }
        else if(!readyToHarvest && water <= 0)
        {
            witherCount -= takingWaterSpeed * Time.deltaTime;
            if(witherCount < 0)
            {
                Wither();
            }
        } 
        
        if(progress > lastChange + 100 / phases && !readyToHarvest)
        {
            lastChange += 100 / phases;
            activePhase++;
            Debug.Log("zmiana fazy "+ activePhase);
            ChangePlantObject(seedPlanted.growthSteps[activePhase]);
        }
        if(test)
        {
            PlantSeeds(seedPlanted);
            test = false;
        }
    }

    void PlantSeeds(Seeds seed)
    {
        if(canPlant)
        {
            seedPlanted = seed;
            progress = 0;
            water = 0;
            readyToHarvest = false;
            canPlant = false;
            waterMin = seed.waterNeeded;
            growthSpeed = seed.growthSpeed;
            phases = seed.growthSteps.Length - 1;
            witherCount = timeToWither = seed.timeToWither;
            ChangePlantObject(seedPlanted.growthSteps[1]);
            activePhase = 1;
            lastChange = 0;
        }
        else
        {
            Debug.Log("Cant plant seeds");
        }
    }
    void Harvest()
    {
        if(readyToHarvest && !withered)
        {
            //here it harvests the plant and adds it to eq
            canPlant = true;
        }
        else
        {
            seedPlanted = null;
            progress = 0;
            water = 0;
            readyToHarvest = false;
            canPlant = true;
            ChangePlantObject(plantDummy);
        }
    }
    void Wither()
    {
        readyToHarvest = true;
        withered = true;
        ChangePlantObject(seedPlanted.growthSteps[0]);
    }
    void ChangePlantObject(GameObject plantObject)
    {
        plantPos = plant.transform.position;
        Destroy(plant);
        plant = Instantiate(plantObject, plantPos, Quaternion.identity);
        plant.transform.parent = gameObject.transform;
        plant.transform.position = plantPos;
    }
    void WaterSeeds(float amount)
    {
        water += amount;
    }

}
