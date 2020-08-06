using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPot : Items
{
    public override void Use()
    {
        if (PlayerStats.currentMana + 30 < PlayerStats.maxMana)
        {
            PlayerStats.currentMana += 30;
        }
        else
        {
            PlayerStats.currentMana = PlayerStats.maxMana;
        }

        Inventory.equipConsume = Inventory.emptyItem;
    }
}
