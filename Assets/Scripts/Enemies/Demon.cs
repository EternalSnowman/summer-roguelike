using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : Boss
{
    public GameObject fireRain;
    public GameObject rightFire;
    public GameObject upFire;
    public GameObject downFire;
    public GameObject leftFire;

    // Update is called once per frame
    void Update()
    {
        Phase1 = false;
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
            if ((downAttack.enabled == false) && (upAttack.enabled == false) && (rightAttack.enabled == false) && (leftAttack.enabled == false) && !Phase1)
            {
                speed = tempSpeed;
            }
        }
        if (room == PlayerStats.room && !Phase1)
        {
            PhaseOne();
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
        if(attackCD > 0)
        {
            attackCD -= Time.deltaTime;
        }
        
        if(skillCD > 0)
        {
            skillCD -= Time.deltaTime;
        }
        
        CheckStatus();
    }

    public override void LoadStats()
    {
        maxHP = 1200 * LVL;
        currentHP = maxHP;
        STR = 80 * LVL;
        INT = 0;
        AGI = 1;
        DEF = 40 * LVL;
        RES = 40 * LVL;

        baseExpYield = 2000 * LVL;

        tempSpeed = 3.5f;
        speed = tempSpeed;
        ATKRNG = 2.5f;

        tempAttackCD = .4f;
        attackCD = tempAttackCD;

        skillCD = 0;
        cycle = 0;

        enemyID = 9;

        fireRain.SetActive(false);
        upFire.SetActive(false);
        downFire.SetActive(false);
        rightFire.SetActive(false);
        leftFire.SetActive(false);
    }

    void PhaseOne()
    {
        enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

        Phase1 = false;

        if (skillCD <= 0)
        {
            skillCD = 5f;
            if (cycle % 2 == 0)
            {
                anim.SetBool("FireRain", true);
            }
            else
            {
                anim.SetBool("FirePulse", true);
            }
            cycle += 1;
        }
    }

    public void FireRain()
    {
        anim.SetBool("FireRain", false);
        GameObject rain = Instantiate(fireRain, target.position, Quaternion.identity);
        rain.SetActive(true);
        rain.GetComponent<DemonFireRain>().damage = STR * 2;
    }

    public void FirePulse()
    {
        anim.SetBool("FirePulse", false);
        GameObject up = Instantiate(upFire, transform.position, Quaternion.identity);
        up.SetActive(true);
        up.GetComponent<DemonFire>().damage = STR * 2;
        up.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5);

        GameObject right = Instantiate(rightFire, transform.position, Quaternion.identity);
        right.SetActive(true);
        right.GetComponent<DemonFire>().damage = STR * 2;
        right.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);

        GameObject left = Instantiate(leftFire, transform.position, Quaternion.identity);
        left.SetActive(true);
        left.GetComponent<DemonFire>().damage = STR * 2;
        left.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);

        GameObject down = Instantiate(downFire, transform.position, Quaternion.identity);
        down.SetActive(true);
        down.GetComponent<DemonFire>().damage = STR * 2;
        down.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -5);
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
            anim.GetCurrentAnimatorStateInfo(0).IsName("FireRainLeft") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("FireRainRight") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("FireRainDown") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("FireRainUp") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("FirePulseLeft") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("FirePulseRight") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("FirePulseDown") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("FirePulseUp"))
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
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("downIdleAttack1"))
        {
            downAttack.enabled = true;
        }
        else
        {
            downAttack.enabled = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("leftIdleAttack1"))
        {
            leftAttack.enabled = true;
        }
        else
        {
            leftAttack.enabled = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("upIdleAttack1"))
        {
            upAttack.enabled = true;
        }
        else
        {
            upAttack.enabled = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("rightIdleAttack1"))
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
