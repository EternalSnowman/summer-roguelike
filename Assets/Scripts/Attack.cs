using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
  // when set true, state machine will transition to attack anim
  public static bool isAttacking;
  // when set true, no other action can be done during this time
  public static bool isSkill;
  public Animator anim;
  public Collider2D upAttack;
  public Collider2D rightAttack;
  public Collider2D leftAttack;
  public Collider2D downAttack;

  public Skill skill1;
  public Skill skill2;
  public Skill skill3;

  public GameObject skill1UI;
  public GameObject skill2UI;
  public GameObject skill3UI;

    // Start is called before the first frame update
    void Start()
  {
    upAttack.enabled = false;
    rightAttack.enabled = false;
    leftAttack.enabled = false;
    downAttack.enabled = false;
    skill1UI = GameObject.FindGameObjectWithTag("Skill1");
    skill2UI = GameObject.FindGameObjectWithTag("Skill2");
    skill3UI = GameObject.FindGameObjectWithTag("Skill3");
    }

    // Update is called once per frame
  void Update()
  {
        skill1UI.GetComponent<Image>().sprite = skill1.icon.sprite;
        skill2UI.GetComponent<Image>().sprite = skill2.icon.sprite;
        skill3UI.GetComponent<Image>().sprite = skill3.icon.sprite;

        skill1UI.GetComponent<Slider>().maxValue = skill1.tempSkillCD;
        skill1UI.GetComponent<Slider>().value = skill1.skillCD;

        skill2UI.GetComponent<Slider>().maxValue = skill2.tempSkillCD;
        skill2UI.GetComponent<Slider>().value = skill2.skillCD;

        skill3UI.GetComponent<Slider>().maxValue = skill3.tempSkillCD;
        skill3UI.GetComponent<Slider>().value = skill3.skillCD;

        // Toggle isAttacking
        if (Input.GetKey(KeyCode.J) && !isAttacking && !isSkill)
       {
           isAttacking = true;

       }
       // Toggle off next frame
       else
       {
            isAttacking = false;
       }

       if (Input.GetKey(KeyCode.K) && !isAttacking && !isSkill && (skill1.skillCD <= 0f) && (PlayerStats.currentMana >= skill1.manaCost))
        {
          skill1.Activate();
          isSkill = true;
        }
       
       else if (Input.GetKey(KeyCode.L) && !isAttacking && !isSkill && (skill2.skillCD <= 0f) && (PlayerStats.currentMana >= skill1.manaCost))
       {
          isSkill = true;
          skill2.Activate();
       }
       else if (Input.GetKey(KeyCode.Semicolon) && !isAttacking && !isSkill && (skill3.skillCD <= 0f) && (PlayerStats.currentMana >= skill1.manaCost))
       {
          isSkill = true;
          skill3.Activate();
       }
       else if(!anim.GetCurrentAnimatorStateInfo(0).IsName(skill1.name) &&
                !anim.GetCurrentAnimatorStateInfo(0).IsName(skill2.name) &&
                !anim.GetCurrentAnimatorStateInfo(0).IsName(skill3.name))
       {
          isSkill = false;
       }

       // Toggle directional attack booleans
       if(anim.GetCurrentAnimatorStateInfo(0).IsName("upIdleAttack") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("upWalkAttack"))
       {
            upAttack.enabled = true;
       }
       else
       {
            upAttack.enabled = false;
       }
       if(anim.GetCurrentAnimatorStateInfo(0).IsName("rightIdleAttack") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("rightWalkAttack"))
       {
            rightAttack.enabled = true;
       }
       else
       {
            rightAttack.enabled = false;
       }
       if(anim.GetCurrentAnimatorStateInfo(0).IsName("leftIdleAttack") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("leftWalkAttack"))
       {
            leftAttack.enabled = true;
       }
       else
       {
            leftAttack.enabled = false;
       }
       if(anim.GetCurrentAnimatorStateInfo(0).IsName("downIdleAttack") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("downWalkAttack"))
       {
            downAttack.enabled = true;
       }
       else
       {
            downAttack.enabled = false;
       }


       anim.SetBool("Attacking", isAttacking);
    }
}
