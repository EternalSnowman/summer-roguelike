using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaterManaPot : Items
{
    private void Start()
    {
        name = "Greater Mana Potion";
        itemDesc = "Restore 50% Mana";
    }

    public override void Use()
    {
        if (PlayerStats.currentMana + (int)Math.Ceiling(PlayerStats.maxMana * 0.5) < PlayerStats.maxMana)
        {
            PlayerStats.currentMana += (int)Math.Ceiling(PlayerStats.maxMana * 0.5);
        }
        else
        {
            PlayerStats.currentMana = PlayerStats.maxMana;
        }

        Inventory.equipConsume = Inventory.emptyItem;
    }
}
