using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BronzeArmor : Items
{
    public int DEF;
    public int RES;

    private void Start()
    {
        name = "Bronze Armor";
        itemDesc = "Increase DEF and RES by 20";
    }


    public override void equipItem()
    {
        PlayerStats.DEF += DEF;
        PlayerStats.RES += RES;
    }

    public override void unequipItem()
    {
        PlayerStats.DEF -= DEF;
        PlayerStats.RES -= RES;
    }
}
