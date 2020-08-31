using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gargoyle : Enemy
{

    public bool attackAnim = false;
    public float speedCtr;
    public float tempSpeedCtr;

    public Collider2D upAttack2;
    public Collider2D rightAttack2;
    public Collider2D leftAttack2;
    public Collider2D downAttack2;

    private void Update()
    {
        
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
            if ((downAttack.enabled == false) && (upAttack.enabled == false) && (rightAttack.enabled == false) && (leftAttack.enabled == false) && (speedCtr <= 0)
                && (downAttack2.enabled == false) && (upAttack2.enabled == false) && (rightAttack2.enabled == false) && (leftAttack2.enabled == false))
            {
                speedCtr = tempSpeedCtr;
                if(speed < 10)
                {
                    speed += tempSpeed;
                }
            }
        }
        if (room == PlayerStats.room)
        {
            AttackCheck();
            if (!isAttacking && attackCD <= 0f)
            {
                EnemyBehavior();
            }
            enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            speed = 0;
        }
        HandleDeath();
        attackCD -= Time.deltaTime;
        speedCtr -= Time.deltaTime;
    }

    public override void LoadStats()
    {
        maxHP = 50 + (LVL * 20);
        currentHP = maxHP;

        STR = 20 + ((LVL - 1) * 8);
        INT = 0;
        AGI = 1;
        DEF = 10 + ((LVL - 1) * 4);
        RES = LVL;

        baseExpYield = 150 + ((LVL - 1) * 30);

        tempSpeed = 1f;
        speed = 0;
        ATKRNG = 1f;
        tempAttackCD = 1f;
        attackCD = tempAttackCD;

        tempSpeedCtr = 1f;
        speedCtr = tempSpeedCtr;

        enemyID = 2;
    }

    public override void AttackCheck()
    {
        // Toggle isAttacking
        if (!flashActive)
        {
            if (((Vector2.Distance(target.position, transform.position) <= ATKRNG) || attackAnim) && (attackCD <= 0f))
            {
                isAttacking = true;
                attackCD = tempAttackCD;
                enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                speed = 0;
            }
            else
            {
                isAttacking = false;
                enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        // Toggle off next frame
        anim.SetBool("Attacking", isAttacking);

        // Keep Gargoyle from moving during attack animation
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("downIdleAttack1") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("downIdleAttack2") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("upIdleAttack1") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("upIdleAttack2") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("leftIdleAttack1") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("leftIdleAttack2") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("rightIdleAttack1") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("rightIdleAttack2"))
        {
            attackAnim = true;
        }
        else
        {
            attackAnim = false;
        }

        // Toggle directional attack booleans
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("downIdleAttack1"))
        {
            downAttack.enabled = true;
            downAttack2.enabled = false;
        }
        else if(anim.GetCurrentAnimatorStateInfo(0).IsName("downIdleAttack2"))
        {
            downAttack.enabled = false;
            downAttack2.enabled = true;
        }
        else
        {
            downAttack.enabled = false;
            downAttack2.enabled = false;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("upIdleAttack1"))
        {
            upAttack.enabled = true;
            upAttack2.enabled = false;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("upIdleAttack2"))
        {
            upAttack.enabled = false;
            upAttack2.enabled = true;
        }
        else
        {
            upAttack.enabled = false;
            upAttack2.enabled = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("leftIdleAttack1"))
        {
            leftAttack.enabled = true;
            leftAttack2.enabled = false;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("leftIdleAttack2"))
        {
            leftAttack.enabled = false;
            leftAttack2.enabled = true;
        }
        else
        {
            leftAttack.enabled = false;
            leftAttack2.enabled = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("rightIdleAttack1"))
        {
            rightAttack.enabled = true;
            rightAttack2.enabled = false;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("rightIdleAttack2"))
        {
            rightAttack.enabled = false;
            rightAttack2.enabled = true;
        }
        else
        {
            rightAttack.enabled = false;
            rightAttack2.enabled = false;
        }

        if ((downAttack.enabled == false) && (upAttack.enabled == false) && (rightAttack.enabled == false) && (leftAttack.enabled == false) && !flashActive
            && (downAttack2.enabled == false) && (upAttack2.enabled == false) && (rightAttack2.enabled == false) && (leftAttack2.enabled == false))
        {
            enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

    }
}
