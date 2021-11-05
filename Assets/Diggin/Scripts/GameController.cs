using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class GameController : MonoBehaviour
{
    public PlayerController playerController;
    public UIController UIcontroller;



    private string world; // worlds name
    private string save_dir; // saving folder

    public float worldTime; //time in seconds in the 24h system
    public float timeSpeed;



    public Light sun;
    public Light moon;
    public Volume skyVolume;
    public AnimationCurve starsCurve;
    private PhysicallyBasedSky sky;
    public bool isNight;

    public float[] digged;
    public float[] diggedYSperarator;
    public float diggedTreshold = 10; // amount needed to spawn digged material
    public Item[] diggedItems;

    public List<GameObject> materialsOnGround = new List<GameObject>();

    public ChunkManager chunkManager;
    public bool isLoaded;
    private void Awake()
    {
        world = (GameObject.FindGameObjectWithTag("DataHolder")).GetComponent<MultiSceneDataHolder>().worldName;
        save_dir = (GameObject.FindGameObjectWithTag("DataHolder")).GetComponent<MultiSceneDataHolder>().Save_Directory;
        playerController = (GameObject.FindGameObjectWithTag("Player")).GetComponent<PlayerController>();
        UIcontroller = gameObject.GetComponent<UIController>();
        skyVolume.profile.TryGet(out sky);
    }
    public bool test;
    public bool test2;
    // Update is called once per frame
    void Update()
    {
        DayNight();
        if (test)
        {
            SaveGame();
        }
        if (test2)
        {
            LoadGame();
        }
        
        if (!isLoaded)
        {
            //Debug.Log(chunkManager.getLoadingProgress());
            UIcontroller.changeLoadingProgressValue((int)chunkManager.getLoadingProgress());
            if (chunkManager.checkIsLoaded())
            {
                UIcontroller.hideLoadingPanel();
                isLoaded = true;
            }
        }
        else
        {
            
        }
    }
    private void OnValidate()
    {
        skyVolume.profile.TryGet(out sky);
    }
    public void SaveGame() //saves buildings, stats, eq, world variables
    {
        //save terrain
        //save buildings
        //save player
        SaveSystem.SavePlayer(playerController,save_dir,world);
    }
    public void RespawnPlayer()
    {
        playerController.RespawnPlayer();
    }
    public void changePlayerPosition(Vector3 pos)
    {
        playerController.gameObject.transform.position = pos;
    }
    public void LoadGame() //loades buildings, stats, eq, world variables
    {
        //load buildings

        //load player data
        SaveData data =SaveSystem.LoadPlayer( save_dir, world);
        playerController.health = data.Health;
    }
    void DayNight()
    {
        CalcTime();
        UIcontroller.displayTime(worldTime);
        float timeAlpha = worldTime / 86400;
        float sunRotation = Mathf.Lerp(-90, 270, worldTime / 86400);
        float moonRotation = sunRotation - 180;
        sun.transform.rotation = Quaternion.Euler(sunRotation, -30, 0);
        moon.transform.rotation = Quaternion.Euler(moonRotation, -30, 0);
        sky.spaceEmissionMultiplier.value = starsCurve.Evaluate(timeAlpha);
        CheckNightDayTransition();
    }
    void CalcTime()
    {
        worldTime += Time.deltaTime * timeSpeed;
        if (worldTime > 86400)
        {
            worldTime -= 86400;
        }
    }
    private void CheckNightDayTransition()
    {
        if(isNight)
        {
            if(moon.transform.rotation.eulerAngles.x >180)
            {
                StartDay();
            }
        }
        else
        {
            if (sun.transform.rotation.eulerAngles.x > 180)
            {
                StartNight();
            }
        }
    }
    private void StartDay()
    {
        isNight = false;
        sun.shadows = LightShadows.Soft;
        moon.shadows = LightShadows.None;
    }
    private void StartNight()
    {
        isNight = true;
        moon.shadows = LightShadows.Soft;
        sun.shadows = LightShadows.None;
    }
    public void AddDiggedGround(float amount,float depth)
    {
        digged[0] -= amount;
        //Debug.Log(depth);
        if(digged[0]>diggedTreshold)
        {
            Debug.Log("spawning");
            playerController.SpawnDiggedMaterial(diggedItems[0]);
            digged[0] += amount;
        }
        //if over set amount drops an item like dirt or something
    }

    public void AddToListOfMaterials(GameObject value)
    {
        materialsOnGround.Add(value);
    }
    public void RemoveFromListOfMaterials(GameObject value)
    {
        materialsOnGround.Remove(value);
    }
}