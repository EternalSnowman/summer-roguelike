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

    public static bool isDisabled;

    public Inventory playerInventory;

    public Skill skill1;
    public Skill skill2;
    public Skill skill3;

    public GameObject skill1UI;
    public GameObject skill2UI;
    public GameObject skill3UI;
    public GameObject consumeUI;
    public GameObject inventoryMenu;
    public GameObject skillMenu;

    public float recentlyOpen;
    public float recentlyOpen2;

    public GameObject tooltip;

    // Start is called before the first frame update
    void Start()
    {
        Skill emptySkill = GameObject.FindGameObjectWithTag("EmptySkill").GetComponent<Skill>();
        skill2 = emptySkill;
        skill3 = emptySkill;

        upAttack.enabled = false;
        rightAttack.enabled = false;
        leftAttack.enabled = false;
        downAttack.enabled = false;

        isDisabled = false;

        skill1UI = GameObject.FindGameObjectWithTag("Skill1");
        skill2UI = GameObject.FindGameObjectWithTag("Skill2");
        skill3UI = GameObject.FindGameObjectWithTag("Skill3");
        consumeUI = GameObject.FindGameObjectWithTag("ConsumableItem");
        inventoryMenu = GameObject.FindGameObjectWithTag("Inventory");
        skillMenu = GameObject.FindGameObjectWithTag("Skill Menu");

        recentlyOpen = 0f;
        recentlyOpen2 = 0f;

        inventoryMenu.SetActive(false);
        skillMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (recentlyOpen >= 0)
        {
            recentlyOpen -= Time.deltaTime;
        }

        if (recentlyOpen2 >= 0)
        {
            recentlyOpen2 -= Time.deltaTime;
        }

        skill1UI.GetComponent<Image>().sprite = skill1.icon.sprite;
        skill2UI.GetComponent<Image>().sprite = skill2.icon.sprite;
        skill3UI.GetComponent<Image>().sprite = skill3.icon.sprite;

        consumeUI.GetComponent<Image>().sprite = Inventory.equipConsume.icon.sprite;

        skill1UI.GetComponent<Slider>().maxValue = skill1.tempSkillCD;
        skill1UI.GetComponent<Slider>().value = skill1.skillCD;

        skill2UI.GetComponent<Slider>().maxValue = skill2.tempSkillCD;
        skill2UI.GetComponent<Slider>().value = skill2.skillCD;

        skill3UI.GetComponent<Slider>().maxValue = skill3.tempSkillCD;
        skill3UI.GetComponent<Slider>().value = skill3.skillCD;
        

        if (Input.GetKey(KeyCode.I) && recentlyOpen <= 0f)
        {
            inventoryMenu.SetActive(!inventoryMenu.activeSelf);
            skillMenu.SetActive(false);
            recentlyOpen = .5f;
        }

        if (Input.GetKey(KeyCode.U) && recentlyOpen2 <= 0f)
        {
            skillMenu.SetActive(!skillMenu.activeSelf);
            inventoryMenu.SetActive(false);
            recentlyOpen2 = .5f;
        }

        // Toggle isAttacking
        if (Input.GetKey(KeyCode.J) && !isAttacking && !isSkill && !isDisabled)
        {
            isAttacking = true;

        }
        // Toggle off next frame
        else
        {
            isAttacking = false;
        }

        if (Input.GetKey(KeyCode.K) && !isAttacking && !isSkill && (skill1.skillCD <= 0f) && (PlayerStats.currentMana >= skill1.manaCost) && !isDisabled)
        {
            skill1.Activate();
            isSkill = true;
        }

        else if (Input.GetKey(KeyCode.L) && !isAttacking && !isSkill && (skill2.skillCD <= 0f) && (PlayerStats.currentMana >= skill2.manaCost) && !isDisabled)
        {
            skill2.Activate();
            isSkill = true;
        }
        else if (Input.GetKey(KeyCode.Semicolon) && !isAttacking && !isSkill && (skill3.skillCD <= 0f) && (PlayerStats.currentMana >= skill3.manaCost) && !isDisabled)
        {
            isSkill = true;
            skill3.Activate();
        }
        else if (!anim.GetCurrentAnimatorStateInfo(0).IsName(skill1.name) &&
                 !anim.GetCurrentAnimatorStateInfo(0).IsName(skill2.name) &&
                 !anim.GetCurrentAnimatorStateInfo(0).IsName(skill3.name))
        {
            isSkill = false;
        }

        if (Input.GetKey(KeyCode.H) && !isAttacking && !isSkill)
        {
            Inventory.equipConsume.Use();
        }

        // Toggle directional attack booleans
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("upIdleAttack") ||
             anim.GetCurrentAnimatorStateInfo(0).IsName("upWalkAttack"))
        {
            upAttack.enabled = true;
        }
        else
        {
            upAttack.enabled = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("rightIdleAttack") ||
             anim.GetCurrentAnimatorStateInfo(0).IsName("rightWalkAttack"))
        {
            rightAttack.enabled = true;
        }
        else
        {
            rightAttack.enabled = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("leftIdleAttack") ||
             anim.GetCurrentAnimatorStateInfo(0).IsName("leftWalkAttack"))
        {
            leftAttack.enabled = true;
        }
        else
        {
            leftAttack.enabled = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("downIdleAttack") ||
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
