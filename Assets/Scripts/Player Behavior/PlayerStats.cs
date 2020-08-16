using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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
    public static int floor;

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
    public static Skill emptySkill;

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
    public static Skill[] learnedSkills;

    public float displayTimer;
    public float tempDisplayTimer;

    public static BuffDebuff emptyBuff;

    public static BuffDebuff[] buffs;

    // Start is called before the first frame update
    void Start()
    {
        BaseStats();

        floor = 1;

        tempDisplayTimer = 2f;
        displayTimer = 0f;

        emptySkill = GameObject.FindGameObjectWithTag("EmptySkill").GetComponent<Skill>();

        learnedSkills = new Skill[12];
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

        miniBossesBeat = new bool[5];
        for (int i = 0; i < miniBossesBeat.Length; i++)
        {
            miniBossesBeat[i] = false;
        }

        GameObject tooltip = GameObject.FindGameObjectWithTag("Tooltip");
        tooltip.SetActive(false);

        SelectionModule selectionModule = GameObject.FindGameObjectWithTag("Selection Module").GetComponent<SelectionModule>();
        selectionModule.gameObject.SetActive(false);

        SkillSelect selectionModule2 = GameObject.FindGameObjectWithTag("SkillSelect").GetComponent<SkillSelect>();
        selectionModule2.gameObject.SetActive(false);
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
            expNext += 100;
            STR += 5;
            maxHP += 10;
            currentHP = maxHP;
            maxMana += 10;
            currentMana = maxMana;
            DEF += 2;
            RES += 2;

            displayTimer = tempDisplayTimer;
            // Bolster
            if(LVL == 3)
            {
                levelUpText.GetComponent<Text>().text = "Level Up to " + LVL + " And Learned Skill Bolster!";
                // TODO: Handle max skills (same for items)
                if (GetFirstEmptySkill() != 12)
                {
                    learnedSkills[GetFirstEmptySkill()] = learnSet[1];
                }

                if(skillRef.skill1 == emptySkill)
                {
                    skillRef.skill1 = learnSet[1];
                }
                else if (skillRef.skill2 == emptySkill)
                {
                    skillRef.skill2 = learnSet[1];
                }
                else if (skillRef.skill3 == emptySkill)
                {
                    skillRef.skill3 = learnSet[1];
                }
            }
            // Quicken
            else if (LVL == 5)
            {
                levelUpText.GetComponent<Text>().text = "Level Up to " + LVL + " And Learned Skill Quicken!";
                if (GetFirstEmptySkill() != 12)
                {
                    learnedSkills[GetFirstEmptySkill()] = learnSet[2];
                }

                if (skillRef.skill1 == emptySkill)
                {
                    skillRef.skill1 = learnSet[2];
                }
                else if (skillRef.skill2 == emptySkill)
                {
                    skillRef.skill2 = learnSet[2];
                }
                else if (skillRef.skill3 == emptySkill)
                {
                    skillRef.skill3 = learnSet[2];
                }
            }
            else
            {
                levelUpText.GetComponent<Text>().text = "Level Up to " + LVL;
            }
        }
    }

    int GetFirstEmptySkill()
    {
        for(int i = 0; i < learnedSkills.Length; i++)
        {
            if(learnedSkills[i] == emptySkill)
            {
                return i;
            }
        }
        // error code
        return 12;
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
            SaveData saveData = new SaveData();
            string path = Application.persistentDataPath + "/data.drm";
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(path))
            {
                FileStream stream = new FileStream(path, FileMode.Open);

                saveData = formatter.Deserialize(stream) as SaveData;
                stream.Close();
            }
            FileStream writeStream = new FileStream(path, FileMode.Create);

            SaveData newData = new SaveData(saveData);

            formatter.Serialize(writeStream, newData);
            writeStream.Close();

            SceneManager.LoadScene("GameOver");
        }
    }

    void BaseStats()
    {
        maxHP = 100;
        currentHP = maxHP;
        maxMana = 100;
        currentMana = maxMana;
        LVL = 1;
        STR = 25;
        DEF = 5;
        RES = 5;
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
        if(buffs[0].currDuration <= 0)
        {
            buff1.GetComponent<Image>().enabled = false;
            buff1.transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
        else
        {
            buff1.GetComponent<Image>().enabled = true;
            buff1.transform.GetChild(0).GetComponent<Image>().enabled = true;
        }

        buff2.GetComponent<Image>().sprite = buffs[1].icon.sprite;
        buff2.GetComponent<Slider>().maxValue = buffs[1].duration;
        buff2.GetComponent<Slider>().value = buffs[1].currDuration;
        if (buffs[1].currDuration <= 0)
        {
            buff2.GetComponent<Image>().enabled = false;
            buff2.transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
        else
        {
            buff2.GetComponent<Image>().enabled = true;
            buff2.transform.GetChild(0).GetComponent<Image>().enabled = true;
        }

        buff3.GetComponent<Image>().sprite = buffs[2].icon.sprite;
        buff3.GetComponent<Slider>().maxValue = buffs[2].duration;
        buff3.GetComponent<Slider>().value = buffs[2].currDuration;
        if (buffs[2].currDuration <= 0)
        {
            buff3.GetComponent<Image>().enabled = false;
            buff3.transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
        else
        {
            buff3.GetComponent<Image>().enabled = true;
            buff3.transform.GetChild(0).GetComponent<Image>().enabled = true;
        }

        buff4.GetComponent<Image>().sprite = buffs[3].icon.sprite;
        buff4.GetComponent<Slider>().maxValue = buffs[3].duration;
        buff4.GetComponent<Slider>().value = buffs[3].currDuration;
        if (buffs[3].currDuration <= 0)
        {
            buff4.GetComponent<Image>().enabled = false;
            buff4.transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
        else
        {
            buff4.GetComponent<Image>().enabled = true;
            buff4.transform.GetChild(0).GetComponent<Image>().enabled = true;
        }

        buff5.GetComponent<Image>().sprite = buffs[4].icon.sprite;
        buff5.GetComponent<Slider>().maxValue = buffs[4].duration;
        buff5.GetComponent<Slider>().value = buffs[4].currDuration;
        if (buffs[4].currDuration <= 0)
        {
            buff5.GetComponent<Image>().enabled = false;
            buff5.transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
        else
        {
            buff5.GetComponent<Image>().enabled = true;
            buff5.transform.GetChild(0).GetComponent<Image>().enabled = true;
        }

        buff6.GetComponent<Image>().sprite = buffs[5].icon.sprite;
        buff6.GetComponent<Slider>().maxValue = buffs[5].duration;
        buff6.GetComponent<Slider>().value = buffs[5].currDuration;
        if (buffs[5].currDuration <= 0)
        {
            buff6.GetComponent<Image>().enabled = false;
            buff6.transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
        else
        {
            buff6.GetComponent<Image>().enabled = true;
            buff6.transform.GetChild(0).GetComponent<Image>().enabled = true;
        }

        buff7.GetComponent<Image>().sprite = buffs[6].icon.sprite;
        buff7.GetComponent<Slider>().maxValue = buffs[6].duration;
        buff7.GetComponent<Slider>().value = buffs[6].currDuration;
        if (buffs[6].currDuration <= 0)
        {
            buff7.GetComponent<Image>().enabled = false;
            buff7.transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
        else
        {
            buff7.GetComponent<Image>().enabled = true;
            buff7.transform.GetChild(0).GetComponent<Image>().enabled = true;
        }

        buff8.GetComponent<Image>().sprite = buffs[7].icon.sprite;
        buff8.GetComponent<Slider>().maxValue = buffs[7].duration;
        buff8.GetComponent<Slider>().value = buffs[7].currDuration;
        if (buffs[7].currDuration <= 0)
        {
            buff8.GetComponent<Image>().enabled = false;
            buff8.transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
        else
        {
            buff8.GetComponent<Image>().enabled = true;
            buff8.transform.GetChild(0).GetComponent<Image>().enabled = true;
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

