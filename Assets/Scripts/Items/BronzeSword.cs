using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BronzeSword : Items
{
    public int STR;

    private void Start()
    {
        adder = 0;
        name = "Bronze Sword";
        itemDesc = "Increase STR by 20";
    }

    public override void equipItem()
    {
        PlayerStats.STR += (STR + adder);
        if(adder > 2)
        {
            itemDesc = "Increase STR by " + (STR + adder) + " (" + (adder / 2) + " Whetstones used)";
        }
        else if(adder == 2)
        {
            itemDesc = "Increase STR by " + (STR + adder) + " (" + (adder / 2) + " Whetstone used)";
        }
    }

    public override void unequipItem()
    {
        PlayerStats.STR -= (STR + adder);
    }
}
