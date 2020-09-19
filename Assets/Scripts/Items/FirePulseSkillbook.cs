using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirePulseSkillbook : Items
{
    public PlayerStats skillRef;

    private void Start()
    {
        name = "Fire Pulse Skillbook";
        itemDesc = "Grant the skill 'Fire Pulse'";

        skillRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    public override void Use()
    {
        skillRef.displayTimer = skillRef.tempDisplayTimer;
        skillRef.levelUpText.GetComponent<Text>().text = "Learned Skill " + skillRef.learnSet[8].name + "!";
        if (skillRef.GetFirstEmptySkill() != 12)
        {
            PlayerStats.learnedSkills[skillRef.GetFirstEmptySkill()] = skillRef.learnSet[8];
        }

        if (skillRef.skillRef.skill1 == PlayerStats.emptySkill)
        {
            skillRef.skillRef.skill1 = skillRef.learnSet[8];
        }
        else if (skillRef.skillRef.skill2 == PlayerStats.emptySkill)
        {
            skillRef.skillRef.skill2 = skillRef.learnSet[8];
        }
        else if (skillRef.skillRef.skill3 == PlayerStats.emptySkill)
        {
            skillRef.skillRef.skill3 = skillRef.learnSet[8];
        }

        Inventory.equipConsume = Inventory.emptyItem;
    }
}
