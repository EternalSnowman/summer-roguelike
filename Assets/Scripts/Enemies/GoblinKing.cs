﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinKing : Boss
{
    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 1 && room == PlayerStats.room)
        {
            PhaseTwo();
        }
        else if (room == PlayerStats.room)
        {
            PhaseOne();

        }
        // Hurtflash
        if (flashActive)
        {
            if (flashCounter > flashLength * .99f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, .1f);
            }
            else if (flashCounter > flashLength * .82f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * .66f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, .1f);
            }
            else if (flashCounter > flashLength * .49f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * .33f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, .1f);
            }
            else if (flashCounter > flashLength * .16f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCounter > 0f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, .1f);
            }
            else
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
                flashActive = false;
            }
            flashCounter -= Time.deltaTime;

        }
        else
        {
            if ((downAttack.enabled == false) && (upAttack.enabled == false) && (rightAttack.enabled == false) && (leftAttack.enabled == false) && !Phase1)
            {
                speed = tempSpeed;
            }
        }
        if (room == PlayerStats.room && !Phase1)
        {
            if (!isAttacking && attackCD <= 0f)
            {
                EnemyBehavior();
            }
            AttackCheck();
            enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            speed = 0;
        }
        HandleDeath();
        attackCD -= Time.deltaTime;

        skillCD -= Time.deltaTime;
        CheckStatus();
    }

    public override void LoadStats()
    {
        maxHP = 1500 * LVL;
        currentHP = maxHP;
        STR = 80 * LVL;
        INT = 0;
        AGI = 1;
        DEF = 100 * LVL;

        baseExpYield = 2000 * LVL;

        tempSpeed = 4.0f;
        speed = tempSpeed;
        ATKRNG = 2;

        tempAttackCD = .4f;
        attackCD = tempAttackCD;

        skillCD = 0;
        cycle = 0;
    }

    void PhaseOne()
    {
        enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        anim.SetBool("Buff", false);
        anim.SetBool("Summon", false);

        speed = 0;
        if (skillCD <= 0)
        {
            skillCD = 5f;
            if (cycle % 2 == 0)
            {
                anim.SetBool("Summon", true);
            }
            else
            {
                Buff();
            }
            cycle += 1;
        }
    }

    void PhaseTwo()
    {
        enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        Phase1 = false;
        if (transition)
        {
            DEF -= 60;
            STR += 10;
            transition = false;
        }
        speed = tempSpeed;
    }

    void Buff()
    {
        anim.SetBool("Buff", true);
        for (var i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().STR += 10 * LVL;
            enemies[i].GetComponent<Enemy>().DEF += 5 * LVL;
            enemies[i].GetComponent<Enemy>().RES += 5 * LVL;

        }

    }

    public void Summon()
    {
        Instantiate(goblin, new Vector3(transform.position.x - 1, transform.position.y, 0), Quaternion.identity);
        Instantiate(goblin, new Vector3(transform.position.x + 1, transform.position.y, 0), Quaternion.identity);
    }

    public override void AttackCheck()
    {
        // Toggle isAttacking
        if (!flashActive)
        {
            if (((Vector2.Distance(target.position, transform.position) <= ATKRNG) || attackAnim) && (attackCD <= 0f) && !Phase1)
            {
                isAttacking = true;
                attackCD = tempAttackCD;
                enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

            }
            else
            {
                isAttacking = false;
                if (!Phase1)
                {
                    enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
            }
        }

        // Keep Orc from moving during attack animation
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("downIdleAttack1") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("downIdleAttack2") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("upIdleAttack1") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("upIdleAttack2") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("leftIdleAttack1") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("leftIdleAttack2") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("rightIdleAttack1") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("rightIdleAttack2") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("Buff") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("Summon"))
        {
            attackAnim = true;
        }
        else
        {
            attackAnim = false;
        }

        // Toggle off next frame
        anim.SetBool("Attacking", isAttacking);

        // Toggle directional attack booleans
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("downIdleAttack2"))
        {
            downAttack.enabled = true;
        }
        else
        {
            downAttack.enabled = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("leftIdleAttack2"))
        {
            leftAttack.enabled = true;
        }
        else
        {
            leftAttack.enabled = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("upIdleAttack2"))
        {
            upAttack.enabled = true;
        }
        else
        {
            upAttack.enabled = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("rightIdleAttack2"))
        {
            rightAttack.enabled = true;
        }
        else
        {
            rightAttack.enabled = false;
        }

        if ((downAttack.enabled == false) && (upAttack.enabled == false) && (rightAttack.enabled == false) && (leftAttack.enabled == false) && !flashActive && !Phase1)
        {
            enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

    }
}
