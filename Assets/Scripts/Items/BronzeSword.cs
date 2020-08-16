using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BronzeSword : Items
{
    public int STR;

    private void Start()
    {
        name = "Bronze Sword";
        itemDesc = "Increase STR by 20";
    }

    public override void equipItem()
    {
        PlayerStats.STR += STR;
    }

    public override void unequipItem()
    {
        PlayerStats.STR -= STR;
    }
}
