using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionModule : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int bagSlot;
    public bool currentlyIn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !currentlyIn)
        {
            gameObject.SetActive(false);
        }
    }

    public void DropButton()
    {
        Inventory.bag[bagSlot].gameObject.SetActive(true);
        Inventory.bag[bagSlot].transform.position = FindObjectOfType<PlayerStats>().transform.position;
        Inventory.bag[bagSlot].gameObject.GetComponent<BoxCollider2D>().enabled = true;
        Inventory.bag[bagSlot].gameObject.GetComponent<SpriteRenderer>().enabled = true;
        Inventory.bag[bagSlot].canPickup = true;
        Inventory.bag[bagSlot] = Inventory.emptyItem;
        gameObject.SetActive(false);
    }

    public void EquipButton()
    {
        // weapon
        if(Inventory.bag[bagSlot].typeOfItem == 1)
        {
            Items temp = Inventory.bag[bagSlot];
            Inventory.bag[bagSlot] = Inventory.equipWeapon;
            Inventory.bag[bagSlot].unequipItem();
            Inventory.equipWeapon = temp;
            Inventory.equipWeapon.equipItem();
        }
        // armor
        else if (Inventory.bag[bagSlot].typeOfItem == 2)
        {
            Items temp = Inventory.bag[bagSlot];
            Inventory.bag[bagSlot] = Inventory.equipArmor;
            Inventory.bag[bagSlot].unequipItem();
            Inventory.equipArmor = temp;
            Inventory.equipArmor.equipItem();
        }
        // consumable
        else if (Inventory.bag[bagSlot].typeOfItem == 3)
        {
            Items temp = Inventory.bag[bagSlot];
            Inventory.bag[bagSlot] = Inventory.equipConsume;
            Inventory.equipConsume = temp;
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
