using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{


    public override void LoadStats()
    {
        LVL = 1;
        maxHP = 30 + (LVL*10);
        currentHP = maxHP;

        STR = 10 + (LVL*3);
        INT = 0;
        AGI = 1;
        DEF = LVL;
        RES = LVL/2;

        baseExpYield = 50 + ((LVL-1)*10);

        tempSpeed = 3.5f;
        speed = tempSpeed;
        ATKRNG = 1;

        tempAttackCD = .2f;
        attackCD = tempAttackCD;

    }
}
