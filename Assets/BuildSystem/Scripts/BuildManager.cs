﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    /// <summary>
    /// This script is not needed for the system to work
    /// In your game this would be the Inventory, HotBar, or some other source
    /// Since I don't have either of those in this example I have chosen to 
    /// simply add this script in as an example of how you could incorperate this system
    /// into your Inventory, HotBar
    /// </summary>


    public GameObject foundationPreview;//make sure that you include the preview of the gameobject you want to build
    public GameObject wallPreview;//make sure that you include the preview of the gameobject you want to build
    public GameObject celingPreview;//make sure that you include the preview of the gameobject you want to build
    public GameObject doorwayPreview;

    public GameObject metalCabinetPreview;
    public GameObject doorPreview;
    public GameObject wallLightPreview;
    public GameObject ceilingLightPreview;

    public BuildSystem buildSystem;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && !buildSystem.isBuilding)//when you press the H key, and the buildingSystem IS NOT currently trying to build something
        {
            buildSystem.NewBuild(foundationPreview);//then you can build a new foundation
        }

        if (Input.GetKeyDown(KeyCode.J) && !buildSystem.isBuilding)
        {
            buildSystem.NewBuild(wallPreview);
        }

        if (Input.GetKeyDown(KeyCode.K) && !buildSystem.isBuilding)
        {
            buildSystem.NewBuild(celingPreview);
        }

        if (Input.GetKeyDown(KeyCode.L) && !buildSystem.isBuilding)
        {
            buildSystem.NewBuild(doorwayPreview);
        }

        if (Input.GetKeyDown(KeyCode.O) && !buildSystem.isBuilding)
        {
            buildSystem.DisableSnapPoints();
        }
        if (Input.GetKeyDown(KeyCode.P) && !buildSystem.isBuilding)
        {
            //buildSystem.EnableSnapPoints();
        }
        if (Input.GetKeyDown(KeyCode.M) && !buildSystem.isBuilding && !buildSystem.isFurniturePlacement)
        {
            buildSystem.NewFurniture(metalCabinetPreview);
        }
        if (Input.GetKeyDown(KeyCode.N) && !buildSystem.isBuilding && !buildSystem.isFurniturePlacement)
        {
            buildSystem.NewFurniture(doorPreview);
        }
        if (Input.GetKeyDown(KeyCode.B) && !buildSystem.isBuilding && !buildSystem.isFurniturePlacement)
        {
            buildSystem.NewFurniture(wallLightPreview);
        }
        if (Input.GetKeyDown(KeyCode.U) && !buildSystem.isBuilding && !buildSystem.isFurniturePlacement)
        {
            buildSystem.NewFurniture(ceilingLightPreview);
        }
    }

}
