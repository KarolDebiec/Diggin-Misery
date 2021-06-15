using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraTerrainModifier : MonoBehaviour
{
    public Text textSize;
    public Text textMaterial;
    [Tooltip("Max range where the player can interact with the terrain")]
    public float rangeHit = 100;
    [Tooltip("Minimal range where the player can interact with the terrain")]
    public float minRangeHit = (float)1.5;
    [Tooltip("Force of modifications applied to the terrain")]
    public float modiferStrengh = 10;
    [Tooltip("Size of the brush, number of vertex modified")]
    public float sizeHit = 6;
    [Tooltip("Color of the new voxels generated")][Range(0, Constants.NUMBER_MATERIALS-1)]
    public int buildingMaterial = 0;

    private RaycastHit hit;
    private ChunkManager chunkManager;
    private GameController gameController;

    private float modified;
    void Awake()
    {
        chunkManager = ChunkManager.Instance;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetMouseButton(0))
        {
            TerraformTarget(rangeHit, minRangeHit, modiferStrengh, sizeHit,false);
        }
        if (Input.GetMouseButton(1))
        {
            TerraformTarget(rangeHit, minRangeHit, modiferStrengh, sizeHit, true);
        }
        else
        {
            modified = 0;
        }*/
        
        //Inputs
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && buildingMaterial != Constants.NUMBER_MATERIALS - 1)
        {
            buildingMaterial++;
            UpdateUI();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && buildingMaterial != 0)
        {
            buildingMaterial--;
            UpdateUI();
        }
        
        if(Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            sizeHit++;
            UpdateUI();
        }
        else if((Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus)) && sizeHit > 1)
        {
            sizeHit--;
            UpdateUI();
        }

    }
    public void TerraformTarget(bool AddTerrain)
    {
        float modification = (AddTerrain) ? modiferStrengh : -modiferStrengh;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rangeHit))
        {
            if (hit.distance > minRangeHit)
            {
                if (hit.collider.CompareTag("Terrain"))
                {
                    chunkManager.ModifyChunkData(hit.point, sizeHit, modification, buildingMaterial);
                    modified += modification;
                    if (modification < 0)
                    {
                        gameController.AddDiggedGround(modification, hit.point.y);
                    }
                    Debug.Log(modified);
                }
            }
        }
    }
    public void UpdateUI()
    {
        textSize.text = "(+ -) Brush size: " + sizeHit;
        textMaterial.text = "(Mouse wheel) Actual material: " + buildingMaterial;
    }
}
