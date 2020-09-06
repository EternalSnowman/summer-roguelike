using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingAuthorityBuff : BuffDebuff
{

    // Start is called before the first frame update
    void Start()
    {
        duration = 10f;
        currDuration = duration;
    }

    public override void Activate()
    {
        base.Activate();
        PlayerMovement.preventDamage = true;
    }

    public override void Deactivate()
    {
        base.Deactivate();

        PlayerMovement.preventDamage = false;
    }
}
