using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedingBuff : BuffDebuff
{
    public int DEFValueTaken;
    // Start is called before the first frame update
    void Start()
    {
        duration = 6f;
        currDuration = duration;
    }

    public override void Activate()
    {
        base.Activate();
        DEFValueTaken = (int)Math.Ceiling(PlayerStats.DEF * 0.2);
        PlayerStats.DEF -= DEFValueTaken;
    }

    public override void Deactivate()
    {
        base.Deactivate();

        PlayerStats.DEF += DEFValueTaken;
    }
}
