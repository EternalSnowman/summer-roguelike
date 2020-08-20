using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBuff : BuffDebuff
{
    public int SpeedValueTaken;
    public Collider2D hitbox;

    // Start is called before the first frame update
    void Start()
    {
        duration = 2f;
        currDuration = duration;
        hitbox.enabled = false;
    }

    public override void Activate()
    {
        base.Activate();

        PlayerMovement.isDisabled = true;
        Attack.isDisabled = true;

        SpeedValueTaken = (int)Math.Ceiling(PlayerMovement.tempSpeed * 0.5);
        PlayerMovement.tempSpeed += SpeedValueTaken;
        hitbox.enabled = true;
    }

    public override void Deactivate()
    {
        base.Deactivate();

        PlayerMovement.isDisabled = false;
        Attack.isDisabled = false;
        PlayerMovement.tempSpeed -= SpeedValueTaken;
        hitbox.enabled = false;
    }
}
