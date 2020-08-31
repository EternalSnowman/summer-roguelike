using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeSlashSkill : Skill
{
    public GameObject projectile;
    
    public bool isSkill;

    // Start is called before the first frame update
    void Start()
    {
        tempSkillCD = 8.0f;
        skillCD = 0f;
        name = "Blade Slash";
        projectile.SetActive(false);
        manaCost = 30;
        isSkill = false;

        skillDesc = "Send a projectile that does Magic damage based on your STR";
    }

    void Update()
    {
        float movex = Input.GetAxis("Horizontal");
        float movey = Input.GetAxis("Vertical");

        anim.SetBool(name, isSkill);

        Vector2 direction = new Vector2(movex, movey);
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("BladeSlashUp") || anim.GetCurrentAnimatorStateInfo(0).IsName("BladeSlashRight") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("BladeSlashDown") || anim.GetCurrentAnimatorStateInfo(0).IsName("BladeSlashLeft"))
        {
            if((movex == 0) && (movey == 0))
            {
                if(anim.GetCurrentAnimatorStateInfo(0).IsName("BladeSlashUp"))
                {
                    direction = new Vector2(0, 1);
                }
                else if(anim.GetCurrentAnimatorStateInfo(0).IsName("BladeSlashDown"))
                {
                    direction = new Vector2(0, -1);
                }
                else if (anim.GetCurrentAnimatorStateInfo(0).IsName("BladeSlashRight"))
                {
                    direction = new Vector2(1, 0);
                }
                else
                {
                    direction = new Vector2(-1, 0);
                }
            } 
            direction.Normalize();
            GameObject slash = (GameObject)Instantiate(projectile, myPos, Quaternion.identity);
            slash.SetActive(true);
            slash.GetComponent<BladeSlashProj>().damage = (int)Math.Ceiling(PlayerStats.STR * 1.5f);
            slash.GetComponent<Rigidbody2D>().velocity = direction * 4;
        }

        if (skillCD > 0)
        {
            skillCD -= Time.deltaTime;
            manaTaken = false;
            isSkill = false;
        }
    }

    public override void Activate()
    {
        base.Activate();
        isSkill = true;
    }
}
