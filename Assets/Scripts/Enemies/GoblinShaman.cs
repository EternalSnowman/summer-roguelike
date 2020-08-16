using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinShaman : Enemy
{
    public GameObject fireball;

    void Start()
    {
        fireball.SetActive(false);
        enemySprite = GetComponent<SpriteRenderer>();
        enemyRigidBody = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerStats>().transform;
        LoadStats();
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
            if (!flashActive && (attackCD <= 0))
            {
                speed = tempSpeed;
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
        if (attackCD > 0)
        {
            attackCD -= Time.deltaTime;
        }
    }

    
    public override void LoadStats()
    {
        maxHP = 120 + (LVL * 10);
        currentHP = maxHP;

        STR = 1;
        INT = 50 + (LVL * 10);
        AGI = 1;
        DEF = LVL;
        RES = LVL * 2;

        baseExpYield = 200 + ((LVL - 1) * 50);

        tempSpeed = 2.5f;
        speed = tempSpeed;
        ATKRNG = 5;

        tempAttackCD = 2f;
        attackCD = tempAttackCD;

    }

    public override void AttackCheck()
    {
        // Toggle isAttacking
        if (!flashActive)
        {
            if (Vector2.Distance(target.position, transform.position) <= ATKRNG && (attackCD <= 0f))
            {
                isAttacking = true;
                attackCD = tempAttackCD;
                speed = 0;

                Vector2 target = new Vector2(FindObjectOfType<PlayerStats>().transform.position.x, FindObjectOfType<PlayerStats>().transform.position.y);
                Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
                Vector2 direction = target - myPos;
                direction.Normalize();
                GameObject projectile = (GameObject)Instantiate(fireball, myPos, Quaternion.identity);
                projectile.SetActive(true);
                projectile.GetComponent<Fireball>().damage = this.INT;
                projectile.GetComponent<Rigidbody2D>().velocity = direction * 3;
            }
            else
            {
                isAttacking = false;
            }
        }

        // Toggle off next frame
        anim.SetBool("Attacking", isAttacking);
        

        if (!flashActive && (attackCD <= 0))
        {
            speed = tempSpeed;
        }

    }
}
