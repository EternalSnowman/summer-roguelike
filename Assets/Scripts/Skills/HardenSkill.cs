using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardenSkill : Skill
{
    public BuffDebuff buff;
    // Start is called before the first frame update
    void Start()
    {
        tempSkillCD = 12f;
        skillCD = 0f;
        name = "Harden";
        manaCost = 20;

        skillDesc = "Decrease Movement Speed by 90% and Increase DEF by 100% for 5 seconds";
    }

    public override void Activate()
    {
        base.Activate();

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
