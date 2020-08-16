using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int skillSlot;
    public Attack skillref;

    public bool currentlyIn;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !currentlyIn)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void EquipSkill1()
    {
        Skill temp = PlayerStats.emptySkill;
        if(skillref.skill1 != PlayerStats.emptySkill)
        {
            temp = skillref.skill1;
        }

        skillref.skill1 = PlayerStats.learnedSkills[skillSlot];

        if(skillref.skill2 == skillref.skill1)
        {
            skillref.skill2 = temp;
        }

        if (skillref.skill3 == skillref.skill1)
        {
            skillref.skill3 = temp;
        }
        gameObject.SetActive(false);
    }

    public void EquipSkill2()
    {
        Skill temp = PlayerStats.emptySkill;
        if (skillref.skill2 != PlayerStats.emptySkill)
        {
            temp = skillref.skill2;
        }

        skillref.skill2 = PlayerStats.learnedSkills[skillSlot];

        if (skillref.skill2 == skillref.skill1)
        {
            skillref.skill1 = temp;
        }

        if (skillref.skill3 == skillref.skill2)
        {
            skillref.skill3 = temp;
        }

        gameObject.SetActive(false);
    }

    public void EquipSkill3()
    {
        Skill temp = PlayerStats.emptySkill;
        if (skillref.skill3 != PlayerStats.emptySkill)
        {
            temp = skillref.skill3;
        }

        skillref.skill3 = PlayerStats.learnedSkills[skillSlot];
        if (skillref.skill3 == skillref.skill1)
        {
            skillref.skill1 = temp;
        }

        if (skillref.skill3 == skillref.skill2)
        {
            skillref.skill2 = temp;
        }

        gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        currentlyIn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        currentlyIn = false;
    }
}
