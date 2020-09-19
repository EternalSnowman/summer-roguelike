using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePulse : Skill
{
    public GameObject rightFire;
    public GameObject upFire;
    public GameObject downFire;
    public GameObject leftFire;

    public bool isSkill;
    public Vector2 direction;
    public Vector2 myPos;
    public bool activated;

    // Start is called before the first frame update
    void Start()
    {
        tempSkillCD = 10.0f;
        skillCD = 0f;
        name = "Fire Pulse";
        upFire.SetActive(false);
        downFire.SetActive(false);
        rightFire.SetActive(false);
        leftFire.SetActive(false);

        manaCost = 30;
        isSkill = false;
        activated = false;

        skillDesc = "Send 4 projectiles in all directions that do Magic damage based on your LVL";
    }

    void Update()
    {
        if (skillCD > 0)
        {
            skillCD -= Time.deltaTime;
            manaTaken = false;
            isSkill = false;
            activated = false;
        }
    }

    public override void Activate()
    {
        base.Activate();
        skillCD = tempSkillCD;
        isSkill = true;

        GameObject up = Instantiate(upFire, transform.position, Quaternion.identity);
        up.SetActive(true);
        up.GetComponent<FirePulseProj>().damage = PlayerStats.LVL * 10;
        up.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5);

        GameObject right = Instantiate(rightFire, transform.position, Quaternion.identity);
        right.SetActive(true);
        right.GetComponent<FirePulseProj>().damage = PlayerStats.LVL * 10;
        right.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);

        GameObject left = Instantiate(leftFire, transform.position, Quaternion.identity);
        left.SetActive(true);
        left.GetComponent<FirePulseProj>().damage = PlayerStats.LVL * 10;
        left.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);

        GameObject down = Instantiate(downFire, transform.position, Quaternion.identity);
        down.SetActive(true);
        down.GetComponent<FirePulseProj>().damage = PlayerStats.LVL * 10;
        down.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -5);
    }
}
