using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    public GameObject snapPoint;
    public GameObject[] snapPoints;
    public bool[] shouldSnapPointBeActive;



    public bool isFirstFoundation;

    public MeshRenderer[] childMeshes;
    public Material basicMat;
    public Material badMat;//red material

    public BuildSystem buildSystem;

    public GameObject buildingGroup;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake()
    {
        buildSystem = (GameObject.FindGameObjectWithTag("BuildSystem")).GetComponent<BuildSystem>();
        buildSystem.AddStructureToList(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void EnableSnapPoints()
    {
        int x = 0;
        foreach (bool shouldSnap in shouldSnapPointBeActive)
        {
            if (shouldSnap)
            {
                snapPoints[x].SetActive(true);
            }
            x++;
        }
    }
    public void EnableSpecificSnapPoints(string snapPointTag)
    {
        int x = 0;
        foreach (bool shouldSnap in shouldSnapPointBeActive)
        {
            if (shouldSnap && snapPoints[x].CompareTag(snapPointTag))
            {
                snapPoints[x].SetActive(true);
            }
            x++;
        }
    }
    public void DisableSnapPoints()
    {
        int x = 0;
        foreach (bool shouldSnap in shouldSnapPointBeActive)
        {
            snapPoints[x].SetActive(false);
            x++;
        }
    }
    public void ChangeColor(bool redColor)//changes between red and greed depending if this preview is/is not able to be placed
    {
        if (redColor)
        {
            foreach (MeshRenderer myMesh in childMeshes)
            {
                myMesh.material = badMat;
            }
        }
        else
        {
            foreach (MeshRenderer myMesh in childMeshes)
            {
                myMesh.material = basicMat;
            }
        }
    }
    public void DestroyStructure()
    {
        buildSystem.RemoveStructureToList(gameObject);
        buildingGroup.GetComponent<buildingGroup>().RemoveFromList(gameObject);
        Destroy(gameObject);
    }
}
