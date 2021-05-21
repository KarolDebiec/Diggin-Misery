using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPointScript : MonoBehaviour
{
    private BuildSystem buildSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Awake()
    {
        buildSystem = GameObject.FindWithTag("BuildSystem").GetComponent<BuildSystem>();
        buildSystem.AddSnapPointToList(gameObject);
    }
    void OnDestroy()
    {
        buildSystem.RemoveSnapPointFromList(gameObject);
    }

    
}
