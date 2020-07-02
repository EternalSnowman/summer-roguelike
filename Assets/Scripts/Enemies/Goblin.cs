﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    

    public override void LoadStats()
    {
        maxHP = 100;
        currentHP = 100;
        speed = 5;
        STR = 10;
    }
}
