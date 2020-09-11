using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
    public bool attackAnim = false;
    public BuffDebuff wet;

    public float jumpCtr;
    public float tempJumpCtr;
    public bool isMoving;

    public override void LoadStats()
    {
        maxHP = 100 + (LVL * 15);
        currentHP = maxHP;

        STR = 40 + ((LVL - 1) * 5);
        INT = 0;
        AGI = 1;
        DEF = 20 + ((LVL - 1) * 2);
        RES = 40 + ((LVL - 1) * 5);

        baseExpYield = 300 + ((LVL - 1) * 50);

        tempSpeed = 7f;
        speed = 0f;
        ATKRNG = 2f;

        tempAttackCD = 0.5f;
        attackCD = tempAttackCD;
        isMoving = false;

        tempJumpCtr = 1f;
        jumpCtr = 0;

        enemyID = 3;
    }

    void Update()
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
            else if(!(anim.GetCurrentAnimatorStateInfo(0).IsName("downWalk") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("upWalk") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("leftWalk") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("rightWalk")))
            {
                isAttacking = false;
                enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        // Keep Orc from moving during attack animation
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("downIdleAttack") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("upIdleAttack") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("leftIdleAttack") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("rightIdleAttack"))
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
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("downIdleAttack"))
        {
            downAttack.enabled = true;
        }
        else
        {
            downAttack.enabled = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("leftIdleAttack"))
        {
            leftAttack.enabled = true;
        }
        else
        {
            leftAttack.enabled = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("upIdleAttack"))
        {
            upAttack.enabled = true;
        }
        else
        {
            upAttack.enabled = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("rightIdleAttack"))
        {
            rightAttack.enabled = true;
        }
        else
        {
            rightAttack.enabled = false;
        }

        if ((downAttack.enabled == false) && (upAttack.enabled == false) && (rightAttack.enabled == false) && (leftAttack.enabled == false) && !flashActive)
        {
            enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

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

    public override void HandleDeath()
    {
        if (currentHP <= 0)
        {
            PlayerStats.EXP += baseExpYield;
            for (int i = 0; i < PlayerStats.buffs.Length; i++)
            {
                if (PlayerStats.buffs[i] == wet)
                {
                    PlayerStats.buffs[i].Deactivate();
                    PlayerStats.buffs[i] = PlayerStats.emptyBuff;
                }
            }
            GameObject.Destroy(gameObject);
        }

    }
}
