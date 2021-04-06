using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : InventoryItemBase
{
    public override string Name
    {
        get
        {
            return "Apple";
        }
    }

    public override void OnUse()
    {
        base.OnUse();
    }

}
