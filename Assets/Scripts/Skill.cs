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
       
    }

    public virtual void Activate()
    {

    }
}
