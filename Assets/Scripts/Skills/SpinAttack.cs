using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttack : Skill
{
    public Collider2D hitbox;
    public int damage;
    public bool isSkill;

    // Start is called before the first frame update
    void Start()
    {
        tempSkillCD = 5.0f;
        skillCD = 0f;
        name = "SpinAttack";
        hitbox.enabled = false;
        manaCost = 20;
        isSkill = false;
    }

    void Update()
    {
        anim.SetBool(name, isSkill);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(name))
        {
            hitbox.enabled = true;
            skillCD = tempSkillCD;
        }
        else
        {
            hitbox.enabled = false;

        }
        if (skillCD > 0)
        {
            skillCD -= Time.deltaTime;
            manaTaken = false;
            isSkill = false;
        }
        damage = (int)Math.Ceiling(PlayerStats.STR * 1.5f);
    }

    public override void Activate()
    {
        base.Activate();
        isSkill = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.isTrigger != true && collision.CompareTag("Enemy"))
        {
            collision.SendMessageUpwards("PhysDamage", damage);
        }

    }
}
