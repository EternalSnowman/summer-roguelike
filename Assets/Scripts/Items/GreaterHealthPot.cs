using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaterHealthPot : Items
{
    private void Start()
    {
        name = "Greater Health Potion";
        itemDesc = "Restore 50% HP";
    }

    public override void Use()
    {
        if (PlayerStats.currentHP + (int)Math.Ceiling(PlayerStats.maxHP * 0.5) < PlayerStats.maxHP)
        {
            PlayerStats.currentHP += (int)Math.Ceiling(PlayerStats.maxHP * 0.5);
        }
        else
        {
            PlayerStats.currentHP = PlayerStats.maxHP;
        }

        Inventory.equipConsume = Inventory.emptyItem;
    }
}