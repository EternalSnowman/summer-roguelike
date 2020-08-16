using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGoblin : Enemy
{
    public override void LoadStats()
    {
        maxHP = 50 + (LVL*35);
        currentHP = maxHP;

        STR = 10 + (LVL*12);
        INT = 0;
        AGI = 1;
        DEF = LVL;
        RES = LVL/2;

        baseExpYield = 0;

        tempSpeed = 3.5f;
        speed = tempSpeed;
        ATKRNG = 1;

        tempAttackCD = .2f;
        attackCD = tempAttackCD;

        // flash goblin on summon
        flashActive = true;
        flashCounter = flashLength;
        speed=0;
    }
}
