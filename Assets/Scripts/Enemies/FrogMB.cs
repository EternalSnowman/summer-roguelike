using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMB : Boss
{
    public BuffDebuff wet;

    public float jumpCtr;
    public float tempJumpCtr;
    public bool isMoving;
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
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
            if ((downAttack.enabled == false) && (upAttack.enabled == false) && (rightAttack.enabled == false) && (leftAttack.enabled == false))
            {
                speed = tempSpeed;
            }
        }

        if(jumpCtr > 0 && !isAttacking)
        {
            jumpCtr -= Time.deltaTime;
        }
        else if(jumpCtr <= 0)
        {
            isMoving = !isMoving;
            jumpCtr = tempJumpCtr;

            anim.SetBool("Moving", isMoving);
        }

        if (isMoving)
        {
            speed = tempSpeed;
        }
        else
        {
            speed = 0;
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
        if (attackCD > 0)
        {
            attackCD -= Time.deltaTime;
        }
    }


    public override void LoadStats()
    {
        maxHP = 900 + (LVL * 160);
        currentHP = maxHP;

        STR = 100 + ((LVL - 1) * 20);
        INT = 0;
        AGI = 1;
        DEF = 20 + ((LVL - 1) * 5);
        RES = 40 + ((LVL - 1) * 5);

        baseExpYield = 1000 + ((LVL - 1) * 200);

        tempSpeed = 7f;
        speed = 0f;
        ATKRNG = 3f;
        tempAttackCD = 1f;
        attackCD = tempAttackCD;
        isMoving = false;

        tempJumpCtr = 1f;
        jumpCtr = 0;

        enemyID = 9;
    }

    // Handle hitbox collision with player hurtbox
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (STR - PlayerStats.DEF < 0)
        {
            if (collision.isTrigger != true && collision.CompareTag("Player"))
            {
                collision.SendMessageUpwards("Damage", 1);
            }
        }
        else if (collision.isTrigger != true && collision.CompareTag("Player"))
        {
            collision.SendMessageUpwards("Damage", STR - PlayerStats.DEF);
        }

        if (collision.isTrigger != true && collision.CompareTag("Player"))
        {
            int value = 8;
            for (int i = 0; i < PlayerStats.buffs.Length; i++)
            {
                if (PlayerStats.buffs[i] == wet)
                {
                    value = i;
                }
            }

            if (value < 8)
            {
                PlayerStats.buffs[value].currDuration = PlayerStats.buffs[value].duration;
            }
            else
            {
                if (PlayerStats.findNextFree() < 8)
                {
                    wet.currDuration = wet.duration;
                    wet.Activate();
                    PlayerStats.buffs[PlayerStats.findNextFree()] = wet;
                }
            }
        }

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

            }
            else
            {
                isAttacking = false;
                enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
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
            anim.GetCurrentAnimatorStateInfo(0).IsName("rightIdleAttack2"))
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
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("downIdleAttack2") || anim.GetCurrentAnimatorStateInfo(0).IsName("leftIdleAttack2")
            || anim.GetCurrentAnimatorStateInfo(0).IsName("upIdleAttack2") || anim.GetCurrentAnimatorStateInfo(0).IsName("rightIdleAttack2"))
        {
            downAttack.enabled = true;
        }
        else
        {
            downAttack.enabled = false;
        }

        if ((downAttack.enabled == false) && (upAttack.enabled == false) && (rightAttack.enabled == false) && (leftAttack.enabled == false) && !flashActive)
        {
            enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

    }
}
