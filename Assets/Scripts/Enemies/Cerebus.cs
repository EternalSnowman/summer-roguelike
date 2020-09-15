using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cerebus : Boss
{
    public BuffDebuff bleeding;

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
        if (room == PlayerStats.room)
        {
            if (!isAttacking && attackCD <= 0f)
            {
                EnemyBehavior();
            }
            AttackCheck();
            enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        HandleDeath();
        if (attackCD > 0)
        {
            attackCD -= Time.deltaTime;
        }
        CheckStatus();
    }

    public override void LoadStats()
    {
        maxHP = 500 + (LVL * 150);
        currentHP = maxHP;

        STR = 80 + ((LVL - 1) * 15);
        INT = 0;
        AGI = 1;
        DEF = 10 + ((LVL - 1) * 5);
        RES = 10 + ((LVL - 1) * 5);

        baseExpYield = 1000 + ((LVL - 1) * 200);

        tempSpeed = 3.5f;
        speed = tempSpeed;
        ATKRNG = 3f;
        tempAttackCD = 1f;
        attackCD = tempAttackCD;

        enemyID = 7;
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
        if (!collision.isTrigger && collision.CompareTag("Player"))
        {
            int value = 8;
            for (int i = 0; i < PlayerStats.buffs.Length; i++)
            {
                if (PlayerStats.buffs[i] == bleeding)
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
                    bleeding.currDuration = bleeding.duration;
                    bleeding.Activate();
                    PlayerStats.buffs[PlayerStats.findNextFree()] = bleeding;
                }
            }
        }
    }

    public override void HandleDeath()
    {
        base.HandleDeath();
        if (currentHP <= 0)
        {
            for (int i = 0; i < PlayerStats.buffs.Length; i++)
            {
                if (PlayerStats.buffs[i] == bleeding)
                {
                    PlayerStats.buffs[i].Deactivate();
                    PlayerStats.buffs[i] = PlayerStats.emptyBuff;
                }
            }
            GameObject.Destroy(gameObject);
        }

    }
}
