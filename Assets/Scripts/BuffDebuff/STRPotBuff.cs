﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STRPotBuff : BuffDebuff
{
    public int STRValueTaken;
    // Start is called before the first frame update
    void Start()
    {
        duration = 8f;
        currDuration = duration;
    }

    public override void Activate()
    {
        base.Activate();
        STRValueTaken = (int)Math.Ceiling(PlayerStats.STR * 0.5);
        PlayerStats.STR += STRValueTaken;
    }

    public override void Deactivate()
    {
        base.Deactivate();

        PlayerStats.STR -= STRValueTaken;
    }
}
