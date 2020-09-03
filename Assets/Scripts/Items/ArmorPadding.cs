using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPadding : Items
{
    private void Start()
    {
        name = "Armor Padding";
        itemDesc = "Increase DEF and RES on active armor by 1";
    }

    public override void Use()
    {
        if (Inventory.equipArmor != Inventory.emptyItem)
        {
            Inventory.equipArmor.unequipItem();
            Inventory.equipArmor.adder += 1;
            Inventory.equipArmor.equipItem();

            Inventory.equipConsume = Inventory.emptyItem;
        }
    }
}
