using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public float maxHP;
    public float currentHP;
    public int LVL;
    public int ATKRNG;
    public int STR;
    public int INT;
    public int AGI;
    public int DEF;
    public int RES;
    public float speed;
    public float tempSpeed;
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

    public bool nearbyPlayer;

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
        tempSpeed = 5;
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
        AttackCheck();
        EnemyBehavior();
        if(currentHP <= 0){
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
        if(collision.isTrigger != true && collision.CompareTag("Player"))
        {
            collision.SendMessageUpwards("Damage", STR);
        }
        /*else if(collision.CompareTag("Weapon"))
        {
            if(!flashActive)
            {
                float x = transform.position.x - collision.transform.position.x;
                float y = transform.position.y - collision.transform.position.y;
                float mod = Mathf.Atan(y/x);

                transform.position = new Vector2(transform.position.x +
                    (knockback * mod* (x/Math.Abs(x))),
                    transform.position.y +
                        (knockback * mod * (y/Math.Abs(y))));

                //enemyRigidBody.AddForce(new Vector2(knockback * Math.Abs(mod) * (x/Math.Abs(x)), knockback * Math.Abs(mod) * (y/Math.Abs(y))));
                //Vector2 moveDirection = transform.position - collision.transform.position;
                //enemyRigidBody.AddForce(moveDirection.normalized * 1000f);
            }
        }*/
    }

    // Handle health reduction
    public void Damage(int damage)
    {
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
            if (Vector2.Distance(target.position, transform.position) <= ATKRNG )
            {
                isAttacking = true;
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
