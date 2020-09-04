using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ghost : Enemy
{
    public float lockout;
    public float tempLockout;
    public bool clone = false;

    // Start is called before the first frame update
    void Start()
    {
        LoadStats();
        enemySprite = GetComponent<SpriteRenderer>();
        target = FindObjectOfType<PlayerStats>().transform;
        upAttack.enabled = false;
    }

    // Update is called once per frame
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

        EnemyBehavior();
        HandleDeath();
        if (lockout > 0)
        {
            lockout -= Time.deltaTime;
        }
    }
    // Default enemy stats
    public override void LoadStats()
    {
        maxHP = 100 + (LVL * 20);

        STR = 40 + (LVL * 4);
        INT = 0;
        AGI = 1;
        DEF = 2 * LVL;
        RES = 5 * LVL;

        if (!clone)
        {
            baseExpYield = 300 + ((LVL - 1) * 50);
            currentHP = maxHP;
        }

        tempSpeed = 2f;
        speed = tempSpeed;

        tempAttackCD = .2f;
        attackCD = tempAttackCD;

        tempLockout = 2f;
        lockout = tempLockout;

        // flash ghost on summon
        flashActive = true;
        flashCounter = flashLength;
        speed = 0;

        // TODO: change this later!
        enemyID = 0;
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
        if (lockout <= 0 && collision.isTrigger != true && collision.CompareTag("Player"))
        {
            lockout = tempLockout;
            Vector3 direction = transform.position - target.transform.position;
            direction.Normalize();

            Ghost clone = Instantiate(this);
            clone.transform.position += direction;
            clone.clone = true;
            clone.baseExpYield = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
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
        if (lockout <= 0 && collision.isTrigger != true && collision.CompareTag("Player"))
        {
            lockout = tempLockout;
            Vector3 direction = transform.position - target.transform.position;
            direction.Normalize();

            Ghost clone = Instantiate(this);
            clone.transform.position += direction;
            clone.clone = true;
            clone.baseExpYield = 0;
        }
    }
}
