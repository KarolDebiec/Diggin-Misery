using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/TerraformingTool")]
public class TerraformingTool : Item
{
    public float rangeHit = 100;
    public float minRangeHit = (float)1.5;
    public float sizeHit = 6;
    public float strenght = 10;
    private CameraTerrainModifier terrainModifier;

    public override void Use()
    {
        terrainModifier = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraTerrainModifier>();
        terrainModifier.TerraformTarget(false);
    }
}
