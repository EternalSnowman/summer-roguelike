using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardenBuff : BuffDebuff
{
    public int DEFValueTaken;
    public float speedValueTaken;

    // Start is called before the first frame update
    void Start()
    {
        duration = 5f;
        currDuration = duration;
    }

    public override void Activate()
    {
        base.Activate();

        DEFValueTaken = PlayerStats.DEF;
        PlayerStats.DEF += DEFValueTaken;

        speedValueTaken = PlayerMovement.tempSpeed * 0.9f;
        PlayerMovement.tempSpeed -= speedValueTaken;
    }

    public override void Deactivate()
    {
        base.Deactivate();

        PlayerStats.DEF -= DEFValueTaken;
        PlayerMovement.tempSpeed += speedValueTaken;
    }
}
