using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public int LVL;
    public float maxHP;
    public float currentHP;
    public int STR;
    public int INT;
    public int AGI;
    public int DEF;
    public int RES;

    public int baseExpYield;
    public float tempSpeed;
    public float speed;
    public float ATKRNG;
    public float knockback;

    public Animator anim;
    public Transform target;
    public Collider2D col;

    public bool isAttacking;
    public Collider2D upAttack;
    public Collider2D rightAttack;
    public Collider2D leftAttack;
    public Collider2D downAttack;

    public bool flashActive;
    public float flashLength = .6f;
    public float flashCounter = 0f;

    public Rigidbody2D enemyRigidBody;
    public SpriteRenderer enemySprite;

    public int room;
    public float tempAttackCD;
    public float attackCD;

    // Start is called before the first frame update
    void Start()
    {
        enemySprite = GetComponent<SpriteRenderer>();
        enemyRigidBody = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerStats>().transform;
        LoadStats();
        upAttack.enabled = false;
        rightAttack.enabled = false;
        leftAttack.enabled = false;
        downAttack.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Hurtflash
        if(flashActive)
        {
            if(flashCounter > flashLength * .99f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, .1f);
            }
            else if (flashCounter > flashLength * .82f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if(flashCounter > flashLength * .66f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, .1f);
            }
            else if (flashCounter > flashLength * .49f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if(flashCounter > flashLength * .33f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, .1f);
            }
            else if (flashCounter > flashLength * .16f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if(flashCounter > 0f)
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
        else{
            if ((downAttack.enabled == false) && (upAttack.enabled == false) && (rightAttack.enabled == false) && (leftAttack.enabled == false))
            {
                speed = tempSpeed;
            }
        }
        if(room == PlayerStats.room){
            if(!isAttacking && attackCD <= 0f)
            {
                EnemyBehavior();
            }
            AttackCheck();
            enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else{
            enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            speed=0;
        }
        HandleDeath();
        attackCD -= Time.deltaTime;
    }

    public void HandleDeath(){
        if(currentHP <= 0){
            PlayerStats.EXP += baseExpYield;
            GameObject.Destroy(gameObject);
        }

    }
    // Default enemy stats
    public virtual void LoadStats() {
        maxHP = 100;
        currentHP = 100;
        speed = 5;
        STR = 10;
        ATKRNG = 1;
        knockback = 1f;
    }

    // Enemy movement
    public virtual void EnemyBehavior(){
        anim.SetFloat("Horizontal", (target.position.x - transform.position.x));
        anim.SetFloat("Vertical", (target.position.y - transform.position.y));
        anim.SetBool("Diagonal", Math.Abs(target.position.x - transform.position.x) > Math.Abs(target.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    // Handle hitbox collision with player hurtbox
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(STR - PlayerStats.DEF < 0)
        {
           if(collision.isTrigger != true && collision.CompareTag("Player"))
            {
                collision.SendMessageUpwards("Damage", 1);
            }
        }
        if(collision.isTrigger != true && collision.CompareTag("Player"))
        {
            collision.SendMessageUpwards("Damage", STR - PlayerStats.DEF);
        }
    }

    public void PhysDamage(int damage)
    {
        damage -= DEF;
        if(damage <= 0)
        {
            damage = 1;
        }
        if(!flashActive)
        {
            if(currentHP >= damage)
            {
                currentHP -= damage;
            }
            else
            {
                currentHP = 0;
            }
            flashActive = true;
            flashCounter = flashLength;
            speed=0;
        }
    }

    public void MagDamage(int damage)
    {
        damage -= RES;
        if(damage <= 0)
        {
            damage = 1;
        }
        if(!flashActive)
        {
            if(currentHP >= damage)
            {
                currentHP -= damage;
            }
            else
            {
                currentHP = 0;
            }
            flashActive = true;
            flashCounter = flashLength;
            speed=0;
        }
    }

    // Check if enemy is close enough to player to start attacking
    public virtual void AttackCheck()
    {
        // Toggle isAttacking
        if(!flashActive)
        {
            if (Vector2.Distance(target.position, transform.position) <= ATKRNG && (attackCD <= 0f))
            {
                isAttacking = true;
                attackCD = tempAttackCD;
                speed = 0;
            }
            else
            {
                isAttacking = false;
            }
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
            speed = tempSpeed;
        }

    }


}
