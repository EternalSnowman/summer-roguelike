using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public static float maxHP;
    public static float currentHP;
    public static int LVL;
    public static int ATKRNG;
    public static int STR;
    public static int INT;
    public static int AGI;
    public static int DEF;
    public static int RES;
    public static float speed;
    public static float tempSpeed;

    public Animator anim;
    public Transform target;
    public Collider2D col;

    public static bool isAttacking;
    public Collider2D upAttack;
    public Collider2D rightAttack;
    public Collider2D leftAttack;
    public Collider2D downAttack;

    // Start is called before the first frame update
    void Start()
    {
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
        AttackCheck();
        EnemyBehavior();
        if(currentHP <= 0){
            GameObject.Destroy(gameObject);
        }
    }

    public virtual void LoadStats() {
        maxHP = 100;
        currentHP = 100;
        speed = 2;
        STR = 10;
    }

    void EnemyBehavior(){
        anim.SetFloat("Horizontal", (target.position.x - transform.position.x));
        anim.SetFloat("Vertical", (target.position.y - transform.position.y));
        anim.SetBool("Diagonal", Math.Abs(target.position.x - transform.position.x) > Math.Abs(target.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.isTrigger != true && collision.CompareTag("Player"))
        {
            collision.SendMessageUpwards("Damage", STR);
        }
    }

    public void Damage(int damage)
    {
        if(currentHP >= damage)
        {
            currentHP -= damage;
        }
        else
        {
            currentHP = 0;
        }
    }

    public void AttackCheck()
    {
        // Toggle isAttacking
        if (Vector2.Distance(target.position, transform.position) <= 1 )
        {
            isAttacking = true;
            speed = 0;
        }
        // Toggle off next frame
        else
        {
            isAttacking = false;
        }

        anim.SetBool("Attacking", isAttacking);


        // Toggle directional attack booleans
        
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("leftIdleAttack"))
        {
            leftAttack.enabled = true;
        }
        else
        {
            leftAttack.enabled = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("downIdleAttack"))
        {
            downAttack.enabled = true;
        }
        else
        {
            downAttack.enabled = false;
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

        if ((downAttack.enabled == false) && (upAttack.enabled == false) && (rightAttack.enabled == false) && (leftAttack.enabled == false))
        {
            speed = tempSpeed;
        }

    }
}
