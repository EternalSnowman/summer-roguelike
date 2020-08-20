using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotItem : Items
{
    public BuffDebuff buff;
    private void Start()
    {
        name = "Speed Potion";
        itemDesc = "Increase Speed by 50% for 5 seconds";
    }

    public override void Use()
    {
        int value = 8;
        for (int i = 0; i < PlayerStats.buffs.Length; i++)
        {
            if (PlayerStats.buffs[i] == buff)
            {
                value = i;
            }
        }

        if (value < 8)
        {
            PlayerStats.buffs[value].currDuration = PlayerStats.buffs[value].duration;
        }
        else
        {
            if (PlayerStats.findNextFree() < 8)
            {
                buff.currDuration = buff.duration;
                buff.Activate();
                PlayerStats.buffs[PlayerStats.findNextFree()] = buff;
            }
        }

        Inventory.equipConsume = Inventory.emptyItem;
    }
}
