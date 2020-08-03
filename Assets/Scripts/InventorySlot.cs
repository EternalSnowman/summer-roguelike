using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public int bagSlot;
    public Image icon;
    public bool equipConsume;
    public bool equipWeapon;
    public bool equipArmor;
    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
