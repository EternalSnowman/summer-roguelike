using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserBinds : MonoBehaviour
{
    // used for static properties
    public static UserBinds UB;

    //Create Keycodes that will be associated with each of our commands.
    //These can be accessed by any other script in our game
    public KeyCode attack {get; set;}
    public KeyCode inventory {get; set;}
    public KeyCode skillsMenu {get; set;}
    public KeyCode skill1 {get; set;}
    public KeyCode skill2 {get; set;}
    public KeyCode skill3 {get; set;}
    public KeyCode useItem {get; set;}
    public KeyCode pickUpItem {get; set;}



    void Awake()
    {
        if(UB == null)
        {
            DontDestroyOnLoad(gameObject);
            UB = this;
        }
        else if(UB != this)
        {
            Destroy(gameObject);
        }

        attack = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("attackKey", "J"));
        inventory = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("inventoryKey", "I"));
        skillsMenu = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("skillsMenuKey", "U"));
        skill1 = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("skill1Key", "K"));
        skill2 = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("skill2Key", "L"));
        skill3 = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("skill3Key", "Semicolon"));
        useItem = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("useItemKey", "H"));
        pickUpItem = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("pickUpKey", "E"));

    }
}
