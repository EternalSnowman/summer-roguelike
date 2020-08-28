using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Will handle control buttons, such as updating text, listening for new key
public class ControlsManager : MonoBehaviour
{
        Transform keyList;
        Event keyEvent;
        Text buttonText;
        KeyCode newKey;

        bool waitingForKey;


        void Start ()
        {
            keyList = transform.Find("KeyList");
            waitingForKey = false;

            for(int i = 0; i < keyList.childCount; i++)
            {
                if(keyList.GetChild(i).name == "AttackKey")
                    keyList.GetChild(i).GetComponentInChildren<Text>().text = UserBinds.UB.attack.ToString();
                else if(keyList.GetChild(i).name == "Skill1Key")
                    keyList.GetChild(i).GetComponentInChildren<Text>().text = UserBinds.UB.skill1.ToString();
                else if(keyList.GetChild(i).name == "Skill2Key")
                    keyList.GetChild(i).GetComponentInChildren<Text>().text = UserBinds.UB.skill2.ToString();
                else if(keyList.GetChild(i).name == "Skill3Key")
                    keyList.GetChild(i).GetComponentInChildren<Text>().text = UserBinds.UB.skill3.ToString();
                else if(keyList.GetChild(i).name == "UseItemKey")
                    keyList.GetChild(i).GetComponentInChildren<Text>().text = UserBinds.UB.useItem.ToString();
                else if(keyList.GetChild(i).name == "InventoryKey")
                    keyList.GetChild(i).GetComponentInChildren<Text>().text = UserBinds.UB.inventory.ToString();
                else if(keyList.GetChild(i).name == "SkillsMenuKey")
                    keyList.GetChild(i).GetComponentInChildren<Text>().text = UserBinds.UB.skillsMenu.ToString();
                else if(keyList.GetChild(i).name == "PickUpItemKey")
                    keyList.GetChild(i).GetComponentInChildren<Text>().text = UserBinds.UB.pickUpItem.ToString();
            }
        }


        void Update ()
        {

        }

        void OnGUI()
        {
            keyEvent = Event.current;

            if(keyEvent.isKey && waitingForKey)
            {
                newKey = keyEvent.keyCode;
                waitingForKey = false;
            }
        }

        /*Buttons cannot call on Coroutines via OnClick().
         * Instead, we have it call StartAssignment, which will
         * call a coroutine in this script instead, only if we
         * are not already waiting for a key to be pressed.
         */
        public void StartAssignment(string keyName)
        {
            if(!waitingForKey)
                StartCoroutine(AssignKey(keyName));
        }


        public void SendText(Text text)
        {
            buttonText = text;
        }

        IEnumerator WaitForKey()
        {
            while(!keyEvent.isKey)
                yield return null;
        }

        /*AssignKey takes a keyName as a parameter. The
         * keyName is checked in a switch statement. Each
         * case assigns the command that keyName represents
         * to the new key that the user presses, which is grabbed
         * in the OnGUI() function, above.
         */
        public IEnumerator AssignKey(string keyName)
        {
            waitingForKey = true;

            yield return WaitForKey();

            switch(keyName)
            {
            case "attack":
                UserBinds.UB.attack = newKey;
                buttonText.text = UserBinds.UB.attack.ToString();
                PlayerPrefs.SetString("attackKey", UserBinds.UB.attack.ToString());
                break;
            case "skill1":
                UserBinds.UB.skill1 = newKey;
                buttonText.text = UserBinds.UB.skill1.ToString();
                PlayerPrefs.SetString("skill1Key", UserBinds.UB.skill1.ToString());
                break;
            case "skill2":
                UserBinds.UB.skill2 = newKey;
                buttonText.text = UserBinds.UB.skill2.ToString();
                PlayerPrefs.SetString("skill2Key", UserBinds.UB.skill2.ToString());
                break;
            case "skill3":
                UserBinds.UB.skill3 = newKey;
                buttonText.text = UserBinds.UB.skill3.ToString();
                PlayerPrefs.SetString("skill3Key", UserBinds.UB.skill3.ToString());
                break;
            case "useItem":
                UserBinds.UB.useItem = newKey;
                buttonText.text = UserBinds.UB.useItem.ToString();
                PlayerPrefs.SetString("useItemKey", UserBinds.UB.useItem.ToString());
                break;
            case "pickUpItem":
                UserBinds.UB.pickUpItem = newKey;
                buttonText.text = UserBinds.UB.pickUpItem.ToString();
                PlayerPrefs.SetString("pickUpKey", UserBinds.UB.pickUpItem.ToString());
                break;
            case "inventory":
                UserBinds.UB.inventory = newKey;
                buttonText.text = UserBinds.UB.inventory.ToString();
                PlayerPrefs.SetString("inventoryKey", UserBinds.UB.inventory.ToString());
                break;
            case "skillsMenu":
                UserBinds.UB.skillsMenu = newKey;
                buttonText.text = UserBinds.UB.skillsMenu.ToString();
                PlayerPrefs.SetString("skillsMenuKey", UserBinds.UB.skillsMenu.ToString());
                break;
            }

            yield return null;
        }
}
