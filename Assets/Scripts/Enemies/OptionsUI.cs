using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltip;
    public BossRewards bossRewards;
    public Options thisOption;

    public bool updateTooltip;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        updateTooltip = true;
        tooltip.SetActive(true);
        if (thisOption.isEXP)
        {
            tooltip.transform.GetChild(1).GetComponent<Text>().text = "EXP";
            tooltip.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = thisOption.extraEXP + " Experience Points";
        }
        else
        {
            tooltip.transform.GetChild(1).GetComponent<Text>().text = thisOption.drop.name;
            tooltip.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = thisOption.drop.itemDesc;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
        updateTooltip = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (updateTooltip)
        {
            tooltip.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y - 50);
        }
    }

    public void HandleClick()
    {
        thisOption.Select();
        Instantiate(bossRewards.stairs, new Vector3(-0.5f, 23f, 0), Quaternion.identity);
        bossRewards.gameObject.SetActive(false);
    }
}
