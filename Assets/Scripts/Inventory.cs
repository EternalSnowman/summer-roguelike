using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Items[] bag;
    public static Items equipWeapon;
    public static Items equipArmor;
    public static Items equipConsume;

    public static Items emptyItem;

    public Items equipDebug;

    // Start is called before the first frame update
    void Start()
    {
        bag = new Items[12];
        emptyItem = GameObject.FindGameObjectWithTag("EmptyItem").GetComponent<Items>();
        for (int i = 0; i < 12; i++)
        {
            bag[i] = emptyItem;
        }
        equipWeapon = emptyItem;
        equipArmor = emptyItem;
        equipConsume = emptyItem;
    }

    // Update is called once per frame
    void Update()
    {
        equipDebug = bag[0];
    }

    public static int GetNextFree()
    {
        for(int i = 0; i < 12; i++)
        {
            if(bag[i] == emptyItem)
            {
                return i;
            }
        }
        // error code
        return 12;
    }
}
