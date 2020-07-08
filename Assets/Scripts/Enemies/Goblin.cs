using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{


    public override void LoadStats()
    {
        maxHP = 40;
        currentHP = maxHP;
        ATKRNG = 1;
        tempSpeed = 5;
        speed = tempSpeed;
        STR = 10;
        knockback = .5f;
    }
}
