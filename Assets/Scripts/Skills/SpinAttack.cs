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
        tempSkillCD = 5.0f;
        skillCD = 0f;
        name = "SpinAttack";
        hitbox.enabled = false;
        damage = 20;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Activate()
    {
        skillCD = tempSkillCD;
        hitbox.enabled = true;
        /*
        if(anim.GetCurrentAnimatorStateInfo(0).IsName(name))
        {

        }
        else
        {
             hitbox.enabled = false;
        }
        */
        //anim.SetBool(name, Attack.isSkill);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.isTrigger != true && collision.CompareTag("Enemy"))
        {
            collision.SendMessageUpwards("PhysDamage", damage);
        }

    }
}
