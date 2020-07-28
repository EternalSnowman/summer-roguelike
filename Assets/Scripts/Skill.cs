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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        skillCD -= Time.deltaTime;
        anim.SetBool(name, Attack.isSkill);
    }

    public virtual void Activate()
    {

    }
}
