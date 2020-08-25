using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public bool isEXP;
    public int extraEXP;
    public Items[] possibleRandomDrops;
    public Items drop;
    public SpriteRenderer icon;

    // Start is called before the first frame update
    void Start()
    {
        if(!isEXP)
        {
            drop = possibleRandomDrops[UnityEngine.Random.Range(0, possibleRandomDrops.Length)];
            icon = drop.icon;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select()
    {
        if(isEXP)
        {
            PlayerStats.EXP += extraEXP;
        }
        else
        {
            if(Inventory.GetNextFree() != 12)
            {
                Items instance = (Items)Instantiate(drop, new Vector3(0, 0, 0), Quaternion.identity);
                Inventory.bag[Inventory.GetNextFree()] = instance;
                instance.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                instance.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                instance.canPickup = false;
            }
            else
            {
                // some error
            }
        }
    }
}
