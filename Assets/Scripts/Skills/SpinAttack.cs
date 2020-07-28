using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttack : Skill
{
    public Collider2D hitbox;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        tempSkillCD = 1.0f;
        skillCD = 0f;
        name = "SpinAttack";
        hitbox.enabled = false;
        manaCost = 10;
    }

    void Update()
    {
        anim.SetBool(name, Attack.isSkill);
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
        }
        damage = (int)Math.Ceiling(PlayerStats.STR * 1.5f);
    }

    public override void Activate()
    {
        if ((PlayerStats.currentMana >= manaCost) && !manaTaken)
        {
            PlayerStats.currentMana -= manaCost;
            manaTaken = true;
        }
        else if(!manaTaken)
        {
            PlayerStats.currentMana = 0;
            manaTaken = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.isTrigger != true && collision.CompareTag("Enemy"))
        {
            collision.SendMessageUpwards("PhysDamage", damage);
        }

    }
}
