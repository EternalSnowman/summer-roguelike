using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingAuthority : Skill
{
    public BuffDebuff buff;
    public SpriteRenderer buffImage;
    public PlayerStats timerRef;

    // Start is called before the first frame update
    void Start()
    {
        tempSkillCD = 15f;
        skillCD = 0f;
        name = "King's Authority";
        manaCost = 20;

        skillDesc = "Become invulnerable for the first hit taken in the next 10 seconds";
    }

    public void Update()
    {
        if (skillCD > 0)
        {
            skillCD -= Time.deltaTime;
            manaTaken = false;
        }

        if (PlayerMovement.preventDamage == false)
        {
            buff.currDuration = 0;
        }
    }

    public override void Activate()
    {
        base.Activate();

        buffImage.sprite = icon.sprite;
        timerRef.timer = timerRef.tempTimer;

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
}
