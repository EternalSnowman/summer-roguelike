using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BronzeArmor : Items
{
    public int DEF;
    public int RES;

    private void Start()
    {
        adder = 0;
        name = "Bronze Armor";
        itemDesc = "Increase DEF and RES by 20";
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
