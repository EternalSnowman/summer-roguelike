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

    public Animator anim;
    public Transform target;
    public Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        loadStats();
        target = FindObjectOfType<PlayerStats>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyBehavior();
        if(currentHP <= 0){
            GameObject.Destroy(gameObject);
        }
    }

    void loadStats(){
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
}
