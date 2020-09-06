using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeSkill : Skill
{
    public BuffDebuff buff;
    public int damage;
    public SpriteRenderer buffImage;
    public PlayerStats timerRef;

    // Start is called before the first frame update
    void Start()
    {
        tempSkillCD = 8f;
        skillCD = 0f;
        name = "Charge";
        manaCost = 30;

        skillDesc = "Charge into a direction until you hit a wall or enemy, dealing damage on impact based on your STR";
    }

    public override void Activate()
    {
        base.Activate();

        buffImage.sprite = icon.sprite;
        timerRef.timer = timerRef.tempTimer;

        damage = (int)Math.Ceiling(PlayerStats.STR * 0.8f);

        skillCD = tempSkillCD;

        int value = 8;
        for (int i = 0; i < PlayerStats.buffs.Length; i++)
        {
            if (PlayerStats.buffs[i] == buff)
            {
                value = i;
            }
        }

        if (value < 8)
        {
            PlayerStats.buffs[value].currDuration = PlayerStats.buffs[value].duration;
        }
        else
        {
            if (PlayerStats.findNextFree() < 8)
            {
                buff.currDuration = buff.duration;
                buff.Activate();
                PlayerStats.buffs[PlayerStats.findNextFree()] = buff;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger != true && collision.CompareTag("Enemy"))
        {
            collision.SendMessageUpwards("PhysDamage", damage);
            buff.currDuration = 0;
        }
        else if (!collision.isTrigger)
        {
            buff.currDuration = 0;
        }

    }
}
