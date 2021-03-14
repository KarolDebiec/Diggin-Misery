using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class GameController : MonoBehaviour
{
    public PlayerController playerController;

    private string world; // worlds name
    private string save_dir; // saving folder

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public bool test;
    public bool test2;
    // Update is called once per frame
    void Update()
    {
        if(test)
        {
            SaveGame();
        }
        if (test2)
        {
            LoadGame();
        }
    }



    private void Awake()
    {
        world = (GameObject.FindGameObjectWithTag("DataHolder")).GetComponent<MultiSceneDataHolder>().worldName;
        save_dir = (GameObject.FindGameObjectWithTag("DataHolder")).GetComponent<MultiSceneDataHolder>().Save_Directory;
    
    }
    public void SaveGame() //saves buildings, stats, eq, world variables
    {
        //save terrain
        //save buildings
        //save player
        SaveSystem.SavePlayer(playerController,save_dir,world);
    }

    public void LoadGame() //loades buildings, stats, eq, world variables
    {
        //load buildings

        //load player data
        SaveData data =SaveSystem.LoadPlayer( save_dir, world);
        playerController.health = data.Health;
    }
}
