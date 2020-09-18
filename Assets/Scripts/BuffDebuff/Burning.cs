using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning : BuffDebuff
{
    public int RESValueTaken;
    // Start is called before the first frame update
    void Start()
    {
        duration = 6f;
        currDuration = duration;
    }

    public override void Activate()
    {
        base.Activate();
        RESValueTaken = (int)Math.Ceiling(PlayerStats.RES * 0.2);
        PlayerStats.RES -= RESValueTaken;
    }

    public override void Deactivate()
    {
        base.Deactivate();

        PlayerStats.RES += RESValueTaken;
    }
}
