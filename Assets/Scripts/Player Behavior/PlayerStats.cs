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

    public static bool[] bossesBeat;
    public static bool[] miniBossesBeat;

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

    public Attack skillRef;
    public Skill emptySkill;

    public GameObject levelUpText;

    public GameObject buff1;
    public GameObject buff2;
    public GameObject buff3;
    public GameObject buff4;
    public GameObject buff5;
    public GameObject buff6;
    public GameObject buff7;
    public GameObject buff8;

    public Skill[] learnSet;
    public Skill[] learnedSkills;

    public float displayTimer;
    public float tempDisplayTimer;

    public static BuffDebuff emptyBuff;

    public static BuffDebuff[] buffs;

    // Start is called before the first frame update
    void Start()
    {
        BaseStats();

        tempDisplayTimer = 2f;
        displayTimer = 0f;

        learnedSkills = new Skill[learnSet.Length];
        learnedSkills[0] = learnSet[0];
        for(int i = 1; i < learnedSkills.Length; i++)
        {
            learnedSkills[i] = emptySkill;
        }

        levelUpText = GameObject.FindGameObjectWithTag("LevelUpText");

        emptyBuff = GameObject.FindGameObjectWithTag("EmptyBuff").GetComponent<BuffDebuff>();
        buffs = new BuffDebuff[8];
        for(int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = emptyBuff;
        }

        bossesBeat = new bool[2];
        for(int i = 0; i < bossesBeat.Length; i++)
        {
            bossesBeat[i] = false;
        }

        miniBossesBeat = new bool[4];
        for (int i = 0; i < miniBossesBeat.Length; i++)
        {
            miniBossesBeat[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        debug();
        CheckStatus();
        HandleLevel();
        BuffDebuffUI();

        if(displayTimer > 0)
        {
            levelUpText.SetActive(true);
            displayTimer -= Time.deltaTime;
        }
        else
        {
            levelUpText.SetActive(false);
        }
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

            displayTimer = tempDisplayTimer;
            if(LVL == 3)
            {
                levelUpText.GetComponent<Text>().text = "Level Up to " + LVL + " And Learned Skill Bolster!";
                learnedSkills[1] = learnSet[1];
                // instead of using 1, in the future we should get the first empty
                skillRef.skill2 = learnedSkills[1];
            }
            else
            {
                levelUpText.GetComponent<Text>().text = "Level Up to " + LVL;
            }
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
        STR = 2000;
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

    void BuffDebuffUI()
    {
        buff1.GetComponent<Image>().sprite = buffs[0].icon.sprite;
        buff1.GetComponent<Slider>().maxValue = buffs[0].duration;
        buff1.GetComponent<Slider>().value = buffs[0].currDuration;
        if(buffs[0].currDuration < 0)
        {
            buff1.GetComponent<Image>().enabled = false;
        }
        else
        {
            buff1.GetComponent<Image>().enabled = true;
        }

        buff2.GetComponent<Image>().sprite = buffs[1].icon.sprite;
        buff2.GetComponent<Slider>().maxValue = buffs[1].duration;
        buff2.GetComponent<Slider>().value = buffs[1].currDuration;
        if (buffs[1].currDuration < 0)
        {
            buff2.GetComponent<Image>().enabled = false;
        }
        else
        {
            buff2.GetComponent<Image>().enabled = true;
        }

        buff3.GetComponent<Image>().sprite = buffs[2].icon.sprite;
        buff3.GetComponent<Slider>().maxValue = buffs[2].duration;
        buff3.GetComponent<Slider>().value = buffs[2].currDuration;
        if (buffs[2].currDuration < 0)
        {
            buff3.GetComponent<Image>().enabled = false;
        }
        else
        {
            buff3.GetComponent<Image>().enabled = true;
        }

        buff4.GetComponent<Image>().sprite = buffs[3].icon.sprite;
        buff4.GetComponent<Slider>().maxValue = buffs[3].duration;
        buff4.GetComponent<Slider>().value = buffs[3].currDuration;
        if (buffs[3].currDuration < 0)
        {
            buff4.GetComponent<Image>().enabled = false;
        }
        else
        {
            buff4.GetComponent<Image>().enabled = true;
        }

        buff5.GetComponent<Image>().sprite = buffs[4].icon.sprite;
        buff5.GetComponent<Slider>().maxValue = buffs[4].duration;
        buff5.GetComponent<Slider>().value = buffs[4].currDuration;
        if (buffs[4].currDuration < 0)
        {
            buff5.GetComponent<Image>().enabled = false;
        }
        else
        {
            buff5.GetComponent<Image>().enabled = true;
        }

        buff6.GetComponent<Image>().sprite = buffs[5].icon.sprite;
        buff6.GetComponent<Slider>().maxValue = buffs[5].duration;
        buff6.GetComponent<Slider>().value = buffs[5].currDuration;
        if (buffs[5].currDuration < 0)
        {
            buff6.GetComponent<Image>().enabled = false;
        }
        else
        {
            buff6.GetComponent<Image>().enabled = true;
        }

        buff7.GetComponent<Image>().sprite = buffs[6].icon.sprite;
        buff7.GetComponent<Slider>().maxValue = buffs[6].duration;
        buff7.GetComponent<Slider>().value = buffs[6].currDuration;
        if (buffs[6].currDuration < 0)
        {
            buff7.GetComponent<Image>().enabled = false;
        }
        else
        {
            buff7.GetComponent<Image>().enabled = true;
        }

        buff8.GetComponent<Image>().sprite = buffs[7].icon.sprite;
        buff8.GetComponent<Slider>().maxValue = buffs[7].duration;
        buff8.GetComponent<Slider>().value = buffs[7].currDuration;
        if (buffs[7].currDuration < 0)
        {
            buff8.GetComponent<Image>().enabled = false;
        }
        else
        {
            buff8.GetComponent<Image>().enabled = true;
        }
    }

    int findNext(int start)
    {
        for(int i = start; i < buffs.Length; i++)
        {
            if(buffs[i] != emptyBuff)
            {
                return i;
            }
        }
        return 8;
    }

    public static int findNextFree()
    {
        for(int i = 0; i < buffs.Length; i++)
        {
            if(buffs[i] == emptyBuff)
            {
                return i;
            }
        }
        return 8;
    }
}

