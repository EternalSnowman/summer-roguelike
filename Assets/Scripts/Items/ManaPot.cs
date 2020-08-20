using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPot : Items
{
    private void Start()
    {
        name = "Mana Potion";
        itemDesc = "Restore 25% Mana";
    }

    public override void Use()
    {
        if (PlayerStats.currentMana + (int)Math.Ceiling(PlayerStats.maxMana * 0.25) < PlayerStats.maxMana)
        {
            PlayerStats.currentMana += (int)Math.Ceiling(PlayerStats.maxMana * 0.25);
        }
        else
        {
            PlayerStats.currentMana = PlayerStats.maxMana;
        }

        Inventory.equipConsume = Inventory.emptyItem;
    }
}
