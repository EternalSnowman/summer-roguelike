using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infection : BuffDebuff
{
    public int STRValueTaken;
    // Start is called before the first frame update
    void Start()
    {
        duration = 6f;
        currDuration = duration;
    }

    public override void Activate()
    {
        base.Activate();
        STRValueTaken = (int)Math.Ceiling(PlayerStats.STR * 0.2);
        PlayerStats.STR -= STRValueTaken;
    }

    public override void Deactivate()
    {
        base.Deactivate();

        PlayerStats.STR += STRValueTaken;
    }
}
