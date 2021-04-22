using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSceneDataHolder : MonoBehaviour
{
    public string worldName;
    public string Save_Directory = "/worlds"; //Directory worlds (save folder)
    void Awake()
    {
        if(GameObject.FindGameObjectsWithTag("DataHolder").Length>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Update()
    {
       // Debug.Log(worldName + " " + Save_Directory +" "+ GameObject.Find("/WorldManager").name);
        //Debug.Log(GameObject.Find("/--- Singletons ---/ChunkManager").name);
    }
}
