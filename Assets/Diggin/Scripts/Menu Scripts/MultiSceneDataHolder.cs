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
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
