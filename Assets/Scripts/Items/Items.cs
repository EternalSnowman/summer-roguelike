using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public SpriteRenderer icon;
    // 1 = weapon, 2 = armor, 3 = consumable
    public int typeOfItem;

    public bool canPickup = false;

    public string name;
    public string itemDesc;

    public int adder;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(UserBinds.UB.pickUpItem) && canPickup)
        {
            if (Inventory.GetNextFree() != 12)
            {
                Inventory.bag[Inventory.GetNextFree()] = this;
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
        if (other.CompareTag("Player"))
        {
            canPickup = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canPickup = false;
        }
    }

    public virtual void equipItem()
    {

    }

    public virtual void unequipItem()
    {

    }
}
