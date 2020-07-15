using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{

    public static float maxHP;
    public static float currentHP;
    public static int LVL;
    public static int STR;
    public static int INT;
    public static int AGI;
    public static int DEF;
    public static int RES;
    public static int EXP;
    public static int expNext;

    public Image healthImage;
    public float healthImageWidth;
    public float healthImageHeight;

    public int level;
    public int currentEXP;
    public int expnext;
    public int strength;
    public int roomNumber;
    public static int room;

    //public static string direction;



    // Start is called before the first frame update
    void Start()
    {
        BaseStats();
        //direction = "down";
        healthImageWidth = healthImage.rectTransform.rect.width;
        healthImageHeight = healthImage.rectTransform.rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        debug();
        CheckHealth();
        HandleLevel();
        healthImage.rectTransform.sizeDelta = new Vector2(Mathf.Lerp(0, healthImageWidth, currentHP / maxHP), healthImageHeight);
        healthImage.color = Color.Lerp(Color.red, Color.green, currentHP / maxHP);

    }

    void HandleLevel()
    {
        if(EXP >= expNext)
        {
            EXP = EXP - expNext;
            LVL++;
            expNext += 50;
            STR += 1;
        }
    }


    void CheckHealth()
    {
        if(currentHP <= 0)
        {
           SceneManager.LoadScene("GameOver");
        }
    }

    void BaseStats()
    {
        maxHP = 1000;
        currentHP = maxHP;
        LVL = 1;
        STR = 20;
        expNext = 100;
        room = 1;

    }

    void debug()
    {
        level = LVL;
        currentEXP = EXP;
        expnext = expNext;
        strength = STR;
        roomNumber = room;

    }
}

