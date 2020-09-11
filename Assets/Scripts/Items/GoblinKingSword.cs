using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinKingSword : Items
{
    public int STR;

    private void Start()
    {
        adder = 0;
        name = "Goblin King's Sword";
        itemDesc = "Increase STR by 50 and increase damage dealt to Goblins by 10%";
    }

    public override void equipItem()
    {
        PlayerStats.STR += (STR + adder);
        if (adder > 2)
        {
            itemDesc = "Increase STR by " + (STR + adder) + "\n(" + (adder / 2) + " Whetstones used)";
        }
        else if (adder == 2)
        {
            itemDesc = "Increase STR by " + (STR + adder) + "\n(" + (adder / 2) + " Whetstone used)";
        }
    }

    public override void unequipItem()
    {
        PlayerStats.STR -= (STR + adder);
    }
}
