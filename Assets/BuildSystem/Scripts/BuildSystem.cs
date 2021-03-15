using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour {

    // yes i know its ceiling and not celing but im too lazy to change that now xD
    public Camera cam;//camera used for raycast
    public LayerMask layer;//the layer that the raycast will hit on
    //////////////////////////////////////////////
    /// 
    /// building system variables  
    /// 

    private GameObject previewGameObject = null;//referance to the preview gameobject
    private Preview previewScript = null;//the Preview.cs script sitting on the previewGameObject

    public float stickTolerance = 1.5f;//used for measuring deviation in the mouse when the buildSystem is paused

    public GameObject buildingGroupPrefab;

    public List<GameObject> allStructureSnapPoints = new List<GameObject>();
    public List<GameObject> allFurnitureSnapPoints = new List<GameObject>();

    public List<GameObject> allStructures = new List<GameObject>();

    [HideInInspector] //hiding this in inspector, so it doesnt accidently get clicked
    public bool isBuilding = false;//are we or are we not currently trying to build something? 
    private bool pauseBuilding = false;//used to pause the raycast
    //destroying
    public GameObject aimedAt;
    public bool isDestroyingBuilding = false;
    //////////////////////////////////////////////
    /// 
    /// furniture placement system variables  
    /// 

    public bool isFurniturePlacement = false;//are we or are we not currently trying to place something? 
    private bool pauseFurniturePlacement = false;//used to pause the raycast
    public GameObject previewFurnitureGameObject = null;//referance to the preview gameobject
    private furniturePreview previewFurnitureScript = null;//the Preview.cs script sitting on the previewGameObject

    public float previewRotationSpeed;

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.R) && isBuilding)//rotate
        {
            previewGameObject.transform.Rotate(0, 90f, 0);//rotate the preview 90 degrees. You can add in your own value here
        }*/
        if (Input.GetKeyDown(KeyCode.V))
        {
            isDestroyingBuilding = true;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            isDestroyingBuilding = false;
            aimedAt.GetComponent<Structure>().ChangeColor(false);
        }
        if (Input.GetKeyDown(KeyCode.G) && isBuilding)//cancel build
        {
            CancelBuild();
        }

        if (Input.GetMouseButtonDown(0) && isBuilding)//actually build the thing in the world
        {
            if (previewScript.GetSnapped())//is the previewGameObject currently snapped to anything? 
            {
                StopBuild();//if so then stop the build and actually build it in the world
            }
            else
            {
                Debug.Log("Not Snapped");//this part isn't needed, but may be good to give your player some feedback if they can't build
            }
        }
        if (Input.GetMouseButtonDown(0) && isDestroyingBuilding)
        {
            if (aimedAt != null)
            {
                DestroyBuilding();
            }
            else
            {
                Debug.Log("Aim at something");
            }
        }
        if (isBuilding)
        {
            if (pauseBuilding)//is the build system currently paused? if so then you need to check deviation in the mouse 
            {
                float mouseX = Input.GetAxis("Mouse X");//get the mouses horizontal movement..these may be different on your copy of unity
                float mouseY = Input.GetAxis("Mouse Y");//get the mouses vertical movement..these may be different on your copy of unity

                if (Mathf.Abs(mouseX) >= stickTolerance || Mathf.Abs(mouseY) >= stickTolerance)//check if mouseX or mouseY is greater than stickTolerance
                {
                    pauseBuilding = false;//if it is, then unpause building, and call the raycast again
                }

            }
            else//if building system isn't paused then call the raycast
            {
                DoBuildRay();
            }
        }

        if (isDestroyingBuilding)
        {
            DoDestroyBuildingRay();
        }

        //////////////////////////////////////////////


        if (Input.GetKey(KeyCode.R) && isFurniturePlacement && previewFurnitureScript.canRotate)//rotate
        {
            previewFurnitureGameObject.transform.Rotate(0, Time.deltaTime*previewRotationSpeed, 0);//rotate the preview x degrees. You can add in your own value here
        }
        if (Input.GetKey(KeyCode.T) && isFurniturePlacement && previewFurnitureScript.canRotate)//rotate
        {
            previewFurnitureGameObject.transform.Rotate(0, -Time.deltaTime * previewRotationSpeed, 0);//rotate the preview -x degrees. You can add in your own value here
        }
        if (Input.GetKeyDown(KeyCode.Q) && isFurniturePlacement && previewFurnitureScript.canRotate && previewFurnitureScript.isSnapped)//rotate
        {
            previewFurnitureScript.snappedTo.transform.Rotate(0, 90, 0);//rotate the preview 90 degrees. You can add in your own value here
        }
        if (Input.GetKeyDown(KeyCode.G) && isFurniturePlacement)//cancel build
        {
            CancelFurniturePlacement();
        }

        if (Input.GetMouseButtonDown(0) && isFurniturePlacement)//actually build the thing in the world
        {
            if(previewFurnitureScript.CanPlace())
            {
                PlaceFurniture();
            }
            else
            {
                Debug.Log("Cannot place");
            }
        }

        if (isFurniturePlacement)
        {
            if (pauseFurniturePlacement)//is the build system currently paused? if so then you need to check deviation in the mouse 
            {
                float mouseX = Input.GetAxis("Mouse X");//get the mouses horizontal movement..these may be different on your copy of unity
                float mouseY = Input.GetAxis("Mouse Y");//get the mouses vertical movement..these may be different on your copy of unity

                if (Mathf.Abs(mouseX) >= stickTolerance || Mathf.Abs(mouseY) >= stickTolerance)//check if mouseX or mouseY is greater than stickTolerance
                {
                    pauseFurniturePlacement = false;//if it is, then unpause building, and call the raycast again
                }

            }
            else//if building system isn't paused then call the raycast
            {
                DoFurniturePlacementRay();
            }
        }
    }


    /// <summary>
    /// However you want to start building with the system. This is the method you would need to call
    /// either from your Inventory, HotBar, or some other source.
    /// You will need to pass in a referance to the previewGameObject that you want to build
    /// </summary>

    public void NewBuild(GameObject _go)
    {
        //temporary solution(i think)
        if(_go.name == "Foundation_Preview")
        {
            EnableSnapPoints("Foundation_SP");
        }
        if (_go.name == "Celing_Preview")
        {
            EnableSnapPoints("Foundation_SP");
            EnableSnapPoints("Celing_SP");
        }
        if (_go.name == "Wall_Preview" || _go.name == "Doorway_Preview")
        {
            EnableSnapPoints("Wall_SP");
        }
        //
        //EnableAllSnapPoints();
        previewGameObject = Instantiate(_go, Vector3.zero, Quaternion.identity);
        previewScript = previewGameObject.GetComponent<Preview>();
        isBuilding = true;
    }

    private void CancelBuild()//you no longer want to build, this will get rid of the previewGameObject in the scene
    {
        Destroy(previewGameObject);
        previewGameObject = null;
        previewScript = null;
        isBuilding = false;
        DisableAllSnapPoints();
    }

    private void StopBuild()//This is a bad name, It should be called something like BuildIt(). This will actually build your preview in the world
    {
        previewScript.Place();
        previewGameObject = null;
        previewScript = null;
        isBuilding = false;
        DisableAllSnapPoints();
    }

    public void PauseBuild(bool _value)//public method to change the pauseBuilding bool from another script. Preview.cs calls this 
                                       //method whereever it snaps to a snap point
    {
        pauseBuilding = _value;
    }

    private void DoBuildRay()//actually positions your previewGameobject in the world
    {
        Ray ray = cam.ScreenPointToRay(new Vector3((Screen.width / 2), (Screen.height / 2)));//raycast stuff  
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 100f, layer))//notice the layer
        {
            /*Since I am using unity primitives in this example I have to postion the previewGameobject in a special way, 
             because of how unity positions things in the scene. If you brought something over from blender, and you have the 
             anchor points setup correctly(located on bottom of model). You can use the line commented out below,
             as opposed to the other lines*/

            //If your using unity primitives use these 3 lines
            /*
            float y = hit.point.y + (previewGameObject.transform.localScale.y / 2f);
            Vector3 pos = new Vector3(hit.point.x, y, hit.point.z);
            previewGameObject.transform.position = pos;
            */
            //if your using something from blender and anchor points are setup correctly use this line
            //previewGameObject.transform.position = hit.point;
            if (hit.collider != null)
            {
                // Add a tag to your object and use use CompareTag for better performace.
                if (hit.collider.CompareTag("Terrain") && previewGameObject.name == "Foundation_Preview(Clone)")
                {
                    float y = hit.point.y + (previewGameObject.transform.localScale.y / 2f);
                    Vector3 pos = new Vector3(hit.point.x, y, hit.point.z);
                    previewGameObject.transform.position = pos;
                    previewGameObject.GetComponent<Preview>().snappedTo = null;
                }
                else if ((hit.collider.CompareTag("Wall_SP") && previewGameObject.name == "Wall_Preview(Clone)") || (hit.collider.CompareTag("Celing_SP") && previewGameObject.name == "Celing_Preview(Clone)") || (hit.collider.CompareTag("Foundation_SP") && previewGameObject.name == "Foundation_Preview(Clone)") || (hit.collider.CompareTag("Wall_SP") && previewGameObject.name == "Doorway_Preview(Clone)"))
                {
                    previewGameObject.GetComponent<Preview>().snappedTo = hit.collider.gameObject;
                    previewGameObject.GetComponent<Preview>().isSnapped = true;
                    previewGameObject.GetComponent<Preview>().canBuild = true;
                    previewGameObject.GetComponent<Preview>().ChangeColor();
                    Vector3 pos = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y, hit.collider.transform.position.z);
                    previewGameObject.transform.position = pos;
                }
                else if (hit.collider.CompareTag("Terrain"))
                {
                    float y = hit.point.y + (previewGameObject.transform.localScale.y / 2f);
                    Vector3 pos = new Vector3(hit.point.x, y, hit.point.z);
                    previewGameObject.transform.position = pos;
                    previewGameObject.GetComponent<Preview>().snappedTo = null;
                    previewGameObject.GetComponent<Preview>().isSnapped = false;
                    previewGameObject.GetComponent<Preview>().canBuild = false;
                    previewGameObject.GetComponent<Preview>().ChangeColor();
                }
            }
        }
    }
    private void DoDestroyBuildingRay()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3((Screen.width / 2), (Screen.height / 2))); 
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, layer))//notice the layer
        {
            if (hit.collider != null)
            {
                // Add a tag to your object and use use CompareTag for better performace.
                if ( aimedAt != null)
                {
                    if (aimedAt != hit.collider.gameObject && !hit.collider.CompareTag("Terrain"))
                    {
                        if (aimedAt.GetComponent<Structure>() != null)
                        {
                            aimedAt.GetComponent<Structure>().ChangeColor(false);
                        }
                    }
                }
                if (!hit.collider.CompareTag("Terrain"))
                {
                    aimedAt = hit.collider.gameObject;
                    aimedAt.GetComponent<Structure>().ChangeColor(true);
                    Debug.Log(hit);
                }
            }
        }
    }
    private void DestroyBuilding()
    {
        aimedAt.GetComponent<Structure>().DestroyStructure();
    }
    public void ObjectPlacement(GameObject _go)
    {
        previewGameObject = Instantiate(_go, Vector3.zero, Quaternion.identity);
        previewScript = previewGameObject.GetComponent<Preview>();
        isBuilding = true;
    }
    public void AddSnapPointToList(GameObject value)
    {
        allStructureSnapPoints.Add(value);
    }
    public void RemoveSnapPointFromList(GameObject value)
    {
        allStructureSnapPoints.Remove(value);
    }
    public void DisableAllSnapPoints()
    {
        foreach (GameObject go in allStructureSnapPoints)
        {
            go.SetActive(false);
        }
    }
    public void EnableAllSnapPoints() // enables all snappoints on scene
    {
        foreach (GameObject go in allStructureSnapPoints)
        {
            go.SetActive(true);
        }
    }
    public void EnableSnapPoints(string tagName) // enables all snappoints on scene that should be enabled
    {
        foreach (GameObject go in allStructures)
        {
            go.GetComponent<Structure>().EnableSpecificSnapPoints(tagName);
        }
    }
    public void DisableSnapPoints()
    {
        foreach (GameObject go in allStructures)
        {
            go.GetComponent<Structure>().DisableSnapPoints();
        }
    }
    public void AddStructureToList(GameObject value)
    {
        allStructures.Add(value);
    }
    public void RemoveStructureToList(GameObject value)
    {
        allStructures.Add(value);
    }
    ///////////////////////////////// \|/ furniture placement
    /// there should be 3 modes for furniture(furniture include all nonstructure objects like shelfs, lamps and even doors or windows)
    /// mode 1 with snap points
    /// mode 2 free placement on foundation
    /// mode 3 free placement on other structures like walls and so on
    ///
    ////////////////////////////////////////////////////////
    ///

    public void NewFurniture(GameObject prev)
    {
        EnableFurnitureSnapPoints();
        previewFurnitureGameObject = Instantiate(prev, Vector3.zero, Quaternion.identity);
        previewFurnitureScript = previewFurnitureGameObject.GetComponent<furniturePreview>();
        previewFurnitureScript.canPlace = false;
        previewFurnitureScript.isSnapped = false;
        previewFurnitureScript.ChangeColor();
        isFurniturePlacement = true;
    }
    private void CancelFurniturePlacement()//you no longer want to build, this will get rid of the previewGameObject in the scene
    {
        Destroy(previewFurnitureGameObject);
        previewFurnitureGameObject = null;
        previewFurnitureScript = null;
        isFurniturePlacement = false;
        DisableFurnitureSnapPoints();
    }

    private void PlaceFurniture()
    {
        previewFurnitureScript.Place();
        previewFurnitureGameObject = null;
        previewFurnitureScript = null;
        isFurniturePlacement = false;
        DisableFurnitureSnapPoints();
    }
    public void PauseFurniturePlacement(bool _value)
    {
        pauseFurniturePlacement = _value;
    }
    public void AddFurnitureSnapPointToList(GameObject value)
    {
        allFurnitureSnapPoints.Add(value);
    }
    public void RemoveFurnitureSnapPointFromList(GameObject value)
    {
        allFurnitureSnapPoints.Remove(value);
    }
    public void DisableFurnitureSnapPoints()
    {
        foreach (GameObject go in allFurnitureSnapPoints)
        {
            go.SetActive(false);
        }
    }
    public void EnableFurnitureSnapPoints()
    {
        foreach (GameObject go in allFurnitureSnapPoints)
        {
            go.SetActive(true);
        }
    }

    private void DoFurniturePlacementRay()//actually positions your previewGameobject in the world
    {
        Ray ray = cam.ScreenPointToRay(new Vector3((Screen.width / 2), (Screen.height / 2)));//raycast stuff
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, layer))//notice the layer
        {
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.tag);
                // Add a tag to your object and use use CompareTag for better performace.
                if (hit.collider.CompareTag("Foundation") && !previewFurnitureScript.shouldBeSnapped)
                {
                    previewFurnitureGameObject.transform.position = hit.point;
                    previewFurnitureScript.isSnapped = false;
                    previewFurnitureScript.canPlace = true;
                }
                else if(hit.collider.CompareTag("Foundation") && previewFurnitureScript.shouldBeSnapped)
                {
                        previewFurnitureGameObject.transform.position = hit.point;
                        previewFurnitureScript.isSnapped = false;
                        previewFurnitureScript.canPlace = false;
                }
                else if ((hit.collider.CompareTag("Door_SP") && previewFurnitureGameObject.name == "door_preview(Clone)"))
                {
                    previewFurnitureScript.snappedTo = hit.collider.gameObject;
                    previewFurnitureScript.isSnapped = true;
                    previewFurnitureScript.ChangeColor();
                    Vector3 pos = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y, hit.collider.transform.position.z);
                    previewFurnitureGameObject.transform.rotation= hit.collider.transform.rotation;
                    previewFurnitureGameObject.transform.position = pos;
                }
                else if ((hit.collider.CompareTag("WallLight_SP") && previewFurnitureGameObject.name == "WallLight_Preview(Clone)"))
                {
                    previewFurnitureScript.snappedTo = hit.collider.gameObject;
                    previewFurnitureScript.isSnapped = true;
                    previewFurnitureScript.ChangeColor();
                    Vector3 pos = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y, hit.collider.transform.position.z);
                    previewFurnitureGameObject.transform.rotation = hit.collider.transform.rotation;
                    previewFurnitureGameObject.transform.position = pos;
                }
                else if ((hit.collider.CompareTag("CeilingLight_SP") && previewFurnitureGameObject.name == "CeilingLight_Preview(Clone)"))
                {
                    previewFurnitureScript.snappedTo = hit.collider.gameObject;
                    previewFurnitureScript.isSnapped = true;
                    previewFurnitureScript.ChangeColor();
                    Vector3 pos = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y, hit.collider.transform.position.z);
                    previewFurnitureGameObject.transform.rotation = hit.collider.transform.rotation;
                    previewFurnitureGameObject.transform.position = pos;
                }
                else
                {
                    previewFurnitureGameObject.transform.position = hit.point;
                    previewFurnitureScript.isSnapped = false;
                    previewFurnitureScript.canPlace = false;
                }
            }
        }
    }

}
