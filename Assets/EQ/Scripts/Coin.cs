using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : InventoryItemBase
{
    public override string Name
    {
        get
        {
            return "gold_Coin";
        }
    }

    public override void OnUse()
    {
        base.OnUse();
    }
}
