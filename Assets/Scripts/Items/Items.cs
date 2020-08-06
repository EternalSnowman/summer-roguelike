using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public SpriteRenderer icon;
    public bool equip;
    // 1 = weapon, 2 = armor, 3 = consumable
    public int typeOfItem;

    public bool canPickup = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.E) && canPickup)
        {
            if (Inventory.GetNextFree() != 12)
            {
                Inventory.bag[Inventory.GetNextFree()] = this;
                //Inventory.equipConsume = this;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                canPickup = false;
            }
            else
            {
                // tell player bag is full
            } 
        }
    }

    public virtual void Use()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        canPickup = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canPickup = false;
    }
}
