using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class furniturePreview : MonoBehaviour
{
    public MeshRenderer[] childMeshes; 
    public Material goodMat;//green material
    public Material badMat;//red material
    public bool isSnapped;
    public bool shouldBeSnapped;
    public bool canPlace = false; // if can place then all other bools are ignored and you can place furniture

    public List<string> tagsISnapTo = new List<string>();
    public GameObject snappedTo;

    public bool canRotate;

    public BuildSystem buildSystem;

    public GameObject furniturePrefab;
    private GameObject builtFurniture = null;
    void Start()
    {
        isSnapped = false;
        canPlace = false;
        buildSystem = GameObject.FindObjectOfType<BuildSystem>();
        ChangeColor();
    }

    
    void Update()
    {
        ChangeColor();
    }

    public void ChangeColor()//changes between red and greed depending if this preview is/is not able to be placed
    {
        
        if (  ( isSnapped && shouldBeSnapped )  || canPlace )
        {
            foreach (MeshRenderer myMesh in childMeshes)
            {
                myMesh.material = goodMat;
            }
        }
        else
        {
            foreach (MeshRenderer myMesh in childMeshes)
            {
                myMesh.material = badMat;
            }
        }
    }
    public void Place()  // places the prefab in world
    {
        builtFurniture = Instantiate(furniturePrefab, transform.position, transform.rotation);
        if(isSnapped && snappedTo != null)
        {
            builtFurniture.transform.parent = snappedTo.transform.parent;
        }
        Destroy(gameObject);
    }
    public void CheckPlace() //checks if can place furniture in that place
    {

    }
    public bool CanPlace()
    {
        if ((isSnapped && shouldBeSnapped)  || canPlace )
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnTriggerEnter(Collider other)//this is what dertermins if you are snapped to a snap point
    {
        if(shouldBeSnapped)
        {
            for (int i = 0; i < tagsISnapTo.Count; i++)//loop through all the tags this preview can snap too
            {
                string currentTag = tagsISnapTo[i];//setting the current tag were looking at to a string...its easier to write currentTag then tagsISnapTo[i]
                if (other.tag == currentTag && other.gameObject.activeSelf)
                {
                    buildSystem.PauseFurniturePlacement(true);//this, and the line below are how you snap
                                                 //since we are using a raycast to position the preview
                                                 //when you snap to something we need to "pause" the raycast
                                                 //otherwise the position will get overridden next frame,
                                                 //and it will look like the preview never snapped to anything
                                                 //pay attention to the stickTolerance and pauseBuilding in the 
                                                 //build system to see how to unpause the build system raycast

                    snappedTo = other.gameObject;
                    transform.position = other.transform.position;//set position of preview so that it "snaps" into position
                    transform.rotation = other.transform.rotation;//set rotation of preview so that it "snaps" into rotation
                    isSnapped = true;//change the bool so we know what color this preview needs to be
                    ChangeColor();
                }
            }
        }  
        
    }

    private void OnTriggerExit(Collider other)//this is what determins if you are no longer snapped to a snap point
    {
        if (shouldBeSnapped) 
        { 
            for (int i = 0; i < tagsISnapTo.Count; i++)//loop through all tags
            {
                string currentTag = tagsISnapTo[i];

                if (other.tag == currentTag)//if we OnTriggerExit something that we can snap too
                {
                    isSnapped = false;//were no longer snapped
                    ChangeColor();//change color
                }
            }
        }
        //Debug.Log(isSnapped + " out " + canBuild);
        ChangeColor(); //change color
    }

}
