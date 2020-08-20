using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabSkill : Skill
{
    public Collider2D upHitbox;
    public Collider2D downHitbox;
    public Collider2D rightHitbox;
    public Collider2D leftHitbox;

    public int damage;
    public bool isSkill;

    // Start is called before the first frame update
    void Start()
    {
        tempSkillCD = 5.0f;
        skillCD = 0f;
        name = "Stab";

        upHitbox.enabled = false;
        downHitbox.enabled = false;
        rightHitbox.enabled = false;
        leftHitbox.enabled = false;

        manaCost = 30;
        isSkill = false;

        skillDesc = "Deal high damage in a small area in front of you based on your STR";
    }

    void Update()
    {
        anim.SetBool(name, isSkill);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("StabUp"))
        {
            upHitbox.enabled = true;
            skillCD = tempSkillCD;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("StabRight"))
        {
            rightHitbox.enabled = true;
            skillCD = tempSkillCD;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("StabLeft"))
        {
            leftHitbox.enabled = true;
            skillCD = tempSkillCD;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("StabDown"))
        {
            downHitbox.enabled = true;
            skillCD = tempSkillCD;
        }
        else
        {
            upHitbox.enabled = false;
            downHitbox.enabled = false;
            rightHitbox.enabled = false;
            leftHitbox.enabled = false;

        }
        if (skillCD > 0)
        {
            skillCD -= Time.deltaTime;
            manaTaken = false;
            isSkill = false;
        }
        damage = (int)Math.Ceiling(PlayerStats.STR * 2f);
    }

    public override void Activate()
    {
        base.Activate();
        isSkill = true;
    }
}
