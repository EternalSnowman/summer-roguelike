using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whetstone : Items
{
    private void Start()
    {
        name = "Whetstone";
        itemDesc = "Increase STR on active weapon by 2";
    }

    public override void Use()
    {
        if(Inventory.equipWeapon != Inventory.emptyItem)
        {
            Inventory.equipWeapon.unequipItem();
            Inventory.equipWeapon.adder += 2;
            Inventory.equipWeapon.equipItem();

            Inventory.equipConsume = Inventory.emptyItem;
        }
    }
}
