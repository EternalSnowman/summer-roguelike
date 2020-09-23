using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPPot : Items
{
    public int expPlus;
    private void Start()
    {
        name = "Health Potion";
        itemDesc = "Restore 25% HP";
    }

    public override void Use()
    {
        PlayerStats.EXP += expPlus;

        Inventory.equipConsume = Inventory.emptyItem;
    }
}
