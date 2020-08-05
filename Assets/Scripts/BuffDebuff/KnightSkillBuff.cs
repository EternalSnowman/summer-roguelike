using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSkillBuff : BuffDebuff
{
    public int STRValueTaken;
    public int DEFValueTaken;
    // Start is called before the first frame update
    void Start()
    {
        duration = 5f;
        currDuration = duration;
    }

    public override void Activate()
    {
        base.Activate();
        STRValueTaken = (int)Math.Ceiling(PlayerStats.STR * 0.2);
        DEFValueTaken = (int)Math.Ceiling(PlayerStats.DEF * 0.2);
        PlayerStats.STR += STRValueTaken;
        PlayerStats.DEF += DEFValueTaken;
    }

    public override void Deactivate()
    {
        base.Deactivate();

        PlayerStats.STR -= STRValueTaken;
        PlayerStats.DEF -= DEFValueTaken;
    }
}
