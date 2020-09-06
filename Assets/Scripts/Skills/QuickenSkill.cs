using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickenSkill : Skill
{
    public BuffDebuff buff;
    public SpriteRenderer buffImage;
    public PlayerStats timerRef;

    // Start is called before the first frame update
    void Start()
    {
        tempSkillCD = 10f;
        skillCD = 0f;
        name = "Quicken";
        manaCost = 20;

        skillDesc = "Increase Movement Speed by 30% for 3 seconds";
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
