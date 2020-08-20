using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPot : Items
{
    private void Start()
    {
        name = "Health Potion";
        itemDesc = "Restore 25% HP";
    }

    public override void Use()
    {
        if(PlayerStats.currentHP + (int)Math.Ceiling(PlayerStats.maxHP * 0.25) < PlayerStats.maxHP)
        {
            PlayerStats.currentHP += (int)Math.Ceiling(PlayerStats.maxHP * 0.25);
        }
        else
        {
            PlayerStats.currentHP = PlayerStats.maxHP;
        }

        Inventory.equipConsume = Inventory.emptyItem;
    }
}
