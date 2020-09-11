using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinKingCrown : Items
{
    public int DEF;
    public int RES;

    private void Start()
    {
        adder = 0;
        name = "Goblin King's Crown";
        itemDesc = "Increase DEF and RES by 40 and increase damage dealt to Goblins by 10%";
    }


    public override void equipItem()
    {
        PlayerStats.DEF += (DEF + adder);
        PlayerStats.RES += (RES + adder);
        if (adder > 1)
        {
            itemDesc = "Increase DEF and RES by " + (DEF + adder) + "\n(" + adder + " Armor Paddings used)";
        }
        else if (adder == 1)
        {
            itemDesc = "Increase DEF and RES by " + (DEF + adder) + "\n(" + adder + " Armor Padding used)";
        }

    }

    public override void unequipItem()
    {
        PlayerStats.DEF -= (DEF + adder);
        PlayerStats.RES -= (RES + adder);
    }
}
