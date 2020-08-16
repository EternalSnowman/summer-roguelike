using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int bagSlot;
    public Image icon;
    public bool equipConsume;
    public bool equipWeapon;
    public bool equipArmor;

    public SelectionModule selectionModule;
    public GameObject tooltip;

    public bool updateTooltip;

    // Start is called before the first frame update
    void Start()
    {
        if(!equipConsume && !equipWeapon && !equipArmor)
        {
            selectionModule = GameObject.FindGameObjectWithTag("Selection Module").GetComponent<SelectionModule>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (equipConsume)
        {
            icon.sprite = Inventory.equipConsume.icon.sprite;
        }
        else if (equipWeapon)
        {
            icon.sprite = Inventory.equipWeapon.icon.sprite;
        }
        else if (equipArmor)
        {
            icon.sprite = Inventory.equipArmor.icon.sprite;
        }
        else
        {
            icon.sprite = Inventory.bag[bagSlot].icon.sprite;
        }

        if (updateTooltip)
        {
            tooltip.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y - 50);
        }
    }

    public void EnableSM()
    {
        if(Inventory.bag[bagSlot] != Inventory.emptyItem)
        {
            selectionModule.gameObject.SetActive(true);
            selectionModule.bagSlot = bagSlot;
            selectionModule.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y - 40);
        } 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (equipConsume && Inventory.equipConsume != Inventory.emptyItem)
        {
            updateTooltip = true;
            tooltip.SetActive(true);
            tooltip.transform.GetChild(1).GetComponent<Text>().text = Inventory.equipConsume.name;
            tooltip.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = Inventory.equipConsume.itemDesc;
        }
        else if (equipWeapon && !equipConsume && (Inventory.equipWeapon != Inventory.emptyItem))
        {
            updateTooltip = true;
            tooltip.SetActive(true);
            tooltip.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = Inventory.equipWeapon.itemDesc;
            tooltip.transform.GetChild(1).GetComponent<Text>().text = Inventory.equipWeapon.name;
        }
        else if (equipArmor && !equipConsume && !equipWeapon && Inventory.equipArmor != Inventory.emptyItem)
        {
            updateTooltip = true;
            tooltip.SetActive(true);
            tooltip.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = Inventory.equipArmor.itemDesc;
            tooltip.transform.GetChild(1).GetComponent<Text>().text = Inventory.equipArmor.name;
        }
        else if(!equipArmor && !equipConsume && !equipWeapon && (Inventory.bag[bagSlot] != Inventory.emptyItem))
        {
            updateTooltip = true;
            tooltip.SetActive(true);
            tooltip.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = Inventory.bag[bagSlot].itemDesc;
            tooltip.transform.GetChild(1).GetComponent<Text>().text = Inventory.bag[bagSlot].name;
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
