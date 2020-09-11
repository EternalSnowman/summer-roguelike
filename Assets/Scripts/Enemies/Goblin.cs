using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    public override void LoadStats()
    {
        maxHP = 30 + (LVL*10);
        currentHP = maxHP;

        STR = 10 + (LVL*6);
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

        enemyID = 0;
    }

    public override void PhysDamage(int damage)
    {
        if (Inventory.equipArmor.name == "Goblin King's Crown" || Inventory.equipWeapon.name == "Goblin King's Sword")
        {
            damage = (int)Math.Ceiling(damage * 1.1f);
        }

        damage -= DEF;

        if (damage <= 0)
        {
            damage = 1;
        }
        if (!flashActive)
        {
            if (currentHP >= damage)
            {
                currentHP -= damage;
            }
            else
            {
                currentHP = 0;
            }
            flashActive = true;
            flashCounter = flashLength;
            speed = 0;
        }
    }

    public override void MagDamage(int damage)
    {
        if (Inventory.equipArmor.name == "Goblin King's Crown" || Inventory.equipWeapon.name == "Goblin King's Sword")
        {
            damage = (int)Math.Ceiling(damage * 1.1f);
        }

        damage -= RES;
        if (damage <= 0)
        {
            damage = 1;
        }
        if (!flashActive)
        {
            if (currentHP >= damage)
            {
                currentHP -= damage;
            }
            else
            {
                currentHP = 0;
            }
            flashActive = true;
            flashCounter = flashLength;
            speed = 0;
        }
    }
}
