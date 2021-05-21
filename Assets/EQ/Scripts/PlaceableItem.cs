using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Placeable")]
public class PlaceableItem : Item
{
    public bool isStructure;
    public bool isFurniture;
    public GameObject placeablePrefab;
    public BuildSystem buildSystem;
    

    public override void Place()
    {
        buildSystem = (GameObject.FindGameObjectWithTag("BuildSystem")).GetComponent<BuildSystem>();
        if (isStructure)
        {
            buildSystem.NewBuild(placeablePrefab);
        }
        else if(isFurniture)
        {
            buildSystem.NewFurniture(placeablePrefab);
        }
        else
        {
            Debug.Log("Not structure nor furniture");
        }
    }
    public override void StopAction() // stops any action that is meant to be used like placing object or building
    {
        buildSystem = (GameObject.FindGameObjectWithTag("BuildSystem")).GetComponent<BuildSystem>();
        if (isStructure)
        {
            buildSystem.CancelBuild();
        }
        else if (isFurniture)
        {
            buildSystem.CancelFurniturePlacement();
        }
        else
        {
            Debug.Log("Not structure nor furniture");
        }
    }

}
