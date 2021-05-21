using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class furnitureSnapPointScript : MonoBehaviour
{
    private BuildSystem buildSystem;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake()
    {
        buildSystem = GameObject.FindWithTag("BuildSystem").GetComponent<BuildSystem>();
        buildSystem.AddFurnitureSnapPointToList(gameObject);
    }
    void OnDestroy()
    {
        buildSystem.RemoveFurnitureSnapPointFromList(gameObject);
    }

}
