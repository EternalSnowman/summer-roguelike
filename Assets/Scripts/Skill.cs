using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float skillCD;
    public float tempSkillCD;
    public String name;
    public Animator anim;
    public int manaCost;
    public bool manaTaken;
    public SpriteRenderer icon;

    // Start is called before the first frame update
    void Start()
    {
        tempSkillCD = 0f;
        skillCD = 0f;
        name = "EmptySkill";
        manaCost = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (skillCD > 0)
        {
            skillCD -= Time.deltaTime;
            manaTaken = false;
        }
    }

    public virtual void Activate()
    {
        if ((PlayerStats.currentMana >= manaCost) && !manaTaken)
        {
            PlayerStats.currentMana -= manaCost;
            manaTaken = true;
        }
        else if (!manaTaken)
        {
            PlayerStats.currentMana = 0;
            manaTaken = true;
        }
    }
}
