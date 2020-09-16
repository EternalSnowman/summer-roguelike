using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cobra : Boss
{
    public GameObject poison;

    public float tempAttackCD2;
    public float attackCD2;
    public float attackCtr;

    void Start()
    {
        poison.SetActive(false);
        //bossRewards.SetActive(false);

        healthBar = GameObject.FindGameObjectWithTag("Boss Health").GetComponent<Slider>();
        enemySprite = GetComponent<SpriteRenderer>();
        enemyRigidBody = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerStats>().transform;
        LoadStats();
    }

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
        if(attackCD2 > 0)
        {
            attackCD2 -= Time.deltaTime;
        }
        CheckStatus();
    }


    public override void LoadStats()
    {
        maxHP = 750 + (LVL * 100);
        currentHP = maxHP;

        STR = 1;
        INT = 100 + (LVL * 20);
        AGI = 1;
        DEF = 10 + (LVL * 3);
        RES = 5 + (LVL * 2);

        baseExpYield = 1000 + ((LVL - 1) * 200);

        tempSpeed = 0f;
        speed = tempSpeed;
        ATKRNG = 100;

        tempAttackCD = .5f;
        attackCD = tempAttackCD;

        tempAttackCD2 = 3f;
        attackCD2 = 0;

        attackCtr = 0;

        enemyID = 8;
    }

    public override void AttackCheck()
    {
        // Toggle isAttacking
        if (!flashActive)
        {
            if(attackCtr >= 4)
            {
                attackCD2 = tempAttackCD2;
                attackCtr = 0;
            }
            else
            {
                if (Vector2.Distance(target.position, transform.position) <= ATKRNG && (attackCD <= 0f) && (attackCD2 <= 0f))
                {
                    isAttacking = true;
                    attackCD = tempAttackCD;
                    speed = 0;
                    attackCtr++;

                    Vector2 target = new Vector2(FindObjectOfType<PlayerStats>().transform.position.x, FindObjectOfType<PlayerStats>().transform.position.y);
                    Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
                    Vector2 direction = target - myPos;
                    direction.Normalize();
                    GameObject projectile = (GameObject)Instantiate(poison, myPos, Quaternion.identity);
                    projectile.SetActive(true);
                    projectile.GetComponent<CobraPoison>().damage = this.INT;
                    projectile.GetComponent<Rigidbody2D>().velocity = direction * 5;
                }
                else
                {
                    isAttacking = false;
                }
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
