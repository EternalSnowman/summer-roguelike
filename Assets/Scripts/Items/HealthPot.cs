using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPot : Items
{
    // Start is called before the first frame update
    void Start()
    {
        Inventory.equipConsume = this;
    }

    public override void Use()
    {
        if(PlayerStats.currentHP + 30 < PlayerStats.maxHP)
        {
            PlayerStats.currentHP += 30;
        }
        else
        {
            PlayerStats.currentHP = PlayerStats.maxHP;
        }

        Inventory.equipConsume = Inventory.emptyItem;
    }
}
