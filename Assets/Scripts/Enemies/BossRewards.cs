using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossRewards : MonoBehaviour
{ 
    public Options option1;
    public Options option2;
    public Options option3;

    public Image option1Image;
    public Image option2Image;
    public Image option3Image;

    public GameObject stairs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        option1Image.sprite = option1.icon.sprite;
        option2Image.sprite = option2.icon.sprite;
        option3Image.sprite = option3.icon.sprite;
        if (Input.GetKey(KeyCode.Alpha1))
        {
            option1.Select();
            gameObject.SetActive(false);
            Instantiate(stairs, new Vector3(-0.5f, 23f, 0), Quaternion.identity);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            option2.Select();
            gameObject.SetActive(false);
            Instantiate(stairs, new Vector3(-0.5f, 23f, 0), Quaternion.identity);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            option3.Select();
            gameObject.SetActive(false);
            Instantiate(stairs, new Vector3(-0.5f, 23f, 0), Quaternion.identity);
        }
    }
}
