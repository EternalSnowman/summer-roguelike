using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int skillSlot;
    public Image icon;
    public Attack skillRef;

    public bool isSkill1;
    public bool isSkill2;
    public bool isSkill3;

    public SkillSelect selectionModule;
    public GameObject tooltip;

    public bool updateTooltip;

    // Start is called before the first frame update
    void Start()
    {
        skillRef = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
        if(!isSkill1 && !isSkill2 && !isSkill3)
        {
            selectionModule = GameObject.FindGameObjectWithTag("SkillSelect").GetComponent<SkillSelect>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSkill1)
        {
            icon.sprite = skillRef.skill1.icon.sprite;
        }
        else if (isSkill2)
        {
            icon.sprite = skillRef.skill2.icon.sprite;
        }
        else if (isSkill3)
        {
            icon.sprite = skillRef.skill3.icon.sprite;
        }
        else
        {
            icon.sprite = PlayerStats.learnedSkills[skillSlot].icon.sprite;
        }

        if (updateTooltip)
        {
            tooltip.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y - 50);
        }
    }

    public void EnableSM()
    {
        if (PlayerStats.learnedSkills[skillSlot] != PlayerStats.emptySkill)
        {
            selectionModule.gameObject.SetActive(true);
            selectionModule.skillSlot = skillSlot;
            selectionModule.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y - 40);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSkill1 && (skillRef.skill1 != PlayerStats.emptySkill))
        {
            tooltip.SetActive(true);
            updateTooltip = true;
            tooltip.transform.GetChild(1).GetComponent<Text>().text = skillRef.skill1.name;
            tooltip.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = skillRef.skill1.skillDesc;
        }
        else if (isSkill2 && (skillRef.skill2 != PlayerStats.emptySkill) && !isSkill1)
        {
            tooltip.SetActive(true);
            updateTooltip = true;
            tooltip.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = skillRef.skill2.skillDesc;
            tooltip.transform.GetChild(1).GetComponent<Text>().text = skillRef.skill2.name;
        }
        else if (isSkill3 && (skillRef.skill3 != PlayerStats.emptySkill) && !isSkill1 && !isSkill2)
        {
            tooltip.SetActive(true);
            updateTooltip = true;
            tooltip.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = skillRef.skill3.skillDesc;
            tooltip.transform.GetChild(1).GetComponent<Text>().text = skillRef.skill3.name;
        }
        else if (PlayerStats.learnedSkills[skillSlot] != PlayerStats.emptySkill && !isSkill1 && !isSkill2 && !isSkill3)
        {
            tooltip.SetActive(true);
            updateTooltip = true;
            tooltip.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = PlayerStats.learnedSkills[skillSlot].skillDesc;
            tooltip.transform.GetChild(1).GetComponent<Text>().text = PlayerStats.learnedSkills[skillSlot].name;
        }
    }

    private void OnDisable()
    {
        updateTooltip = false;
        tooltip.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
        updateTooltip = false;
    }
}
