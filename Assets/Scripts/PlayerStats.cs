using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{

    public static float maxHP;
    public static float currentHP;
    public static float maxMana;
    public static float currentMana;
    public static int LVL;
    public static int STR;
    public static int INT;
    public static int AGI;
    public static int DEF;
    public static int RES;
    public static int EXP;
    public static int expNext;

    public Slider healthSlider;
    public Slider manaSlider;
    public Slider expSlider;

    public Text levelText;

    public int level;
    public int currentEXP;
    public int expnext;
    public int strength;
    public int roomNumber;
    public static int room;

    // Start is called before the first frame update
    void Start()
    {
        BaseStats();
    }

    // Update is called once per frame
    void Update()
    {
        debug();
        CheckStatus();
        HandleLevel();
    }

    void HandleLevel()
    {
        if(EXP >= expNext)
        {
            EXP = EXP - expNext;
            LVL++;
            expNext += 50;
            STR += 5;
            maxHP += 20;
            currentHP = maxHP;
            maxMana += 20;
            currentMana = maxMana;
        }
    }


    void CheckStatus()
    {
        healthSlider.value = currentHP;
        healthSlider.maxValue = maxHP;

        manaSlider.value = currentMana;
        manaSlider.maxValue = maxMana;

        expSlider.value = EXP;
        expSlider.maxValue = expNext;

        levelText.text = LVL.ToString();

        if(currentHP <= 0)
        {
           SceneManager.LoadScene("GameOver");
        }
    }

    void BaseStats()
    {
        maxHP = 10000;
        currentHP = maxHP;
        maxMana = 100;
        currentMana = maxMana;
        LVL = 1;
        STR = 20;
        expNext = 500;
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

