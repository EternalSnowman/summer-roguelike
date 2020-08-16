using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickenBuff : BuffDebuff
{
    public float SPDValueTaken;
    // Start is called before the first frame update
    void Start()
    {
        duration = 3f;
        currDuration = duration;
    }

    public override void Activate()
    {
        base.Activate();
        SPDValueTaken = PlayerMovement.tempSpeed * 0.3f;
        PlayerMovement.tempSpeed += SPDValueTaken;
    }

    public override void Deactivate()
    {
        base.Deactivate();

        PlayerMovement.tempSpeed -= SPDValueTaken;
    }
}
