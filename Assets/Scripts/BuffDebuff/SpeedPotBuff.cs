using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotBuff : BuffDebuff
{
    public int SpeedValueTaken;

    // Start is called before the first frame update
    void Start()
    {
        duration = 5f;
        currDuration = duration;
    }

    public override void Activate()
    {
        base.Activate();
        SpeedValueTaken = (int)Math.Ceiling(PlayerMovement.tempSpeed * 0.5);
        PlayerMovement.tempSpeed += SpeedValueTaken;
    }

    public override void Deactivate()
    {
        base.Deactivate();

        PlayerMovement.tempSpeed -= SpeedValueTaken;
    }
}
