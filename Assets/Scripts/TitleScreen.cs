using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public SaveData saveData;
    public GameObject settingsPanel;
    public GameObject dungeonJournal;
    public GameObject loadingScreen;

    public Sprite emptyClass;
    public Sprite knight;
    public Sprite mage;
    public Sprite hunter;

    // Start is called before the first frame update
    void Start()
    {
        settingsPanel = GameObject.FindGameObjectWithTag("Skill Menu");
        settingsPanel.SetActive(false);

        loadingScreen.SetActive(false);

        dungeonJournal = GameObject.FindGameObjectWithTag("LevelUpText");
        dungeonJournal.SetActive(false);

        string path = Application.persistentDataPath + "/data.drm";
        BinaryFormatter formatter = new BinaryFormatter();
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);

            saveData = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            AudioListener.volume = saveData.musicVolume;
            Screen.fullScreen = saveData.fullScreen;
            if (saveData.resolution == 1)
            {
                Screen.SetResolution(1024, 576, Screen.fullScreen);
            }
            else if (saveData.resolution == 3)
            {
                Screen.SetResolution(1600, 900, Screen.fullScreen);
            }
            else if (saveData.resolution == 4)
            {
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
            }
            else
            {
                Screen.SetResolution(1280, 720, Screen.fullScreen);
            }
        }
        else
        {
            FileStream stream = new FileStream(path, FileMode.Create);

            SaveData empty = new SaveData();

            formatter.Serialize(stream, empty);
            stream.Close();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dungeonJournal.activeSelf)
        {
            for(int i = 0; i < 5; i++)
            {
                switch(saveData.mostRecentClasses[i])
                {
                    case 0:
                        dungeonJournal.transform.GetChild(1).GetChild(i).GetComponent<Image>().sprite = emptyClass;
                        break;
                    case 1:
                        dungeonJournal.transform.GetChild(1).GetChild(i).GetComponent<Image>().sprite = knight;
                        break;
                    case 2:
                        dungeonJournal.transform.GetChild(1).GetChild(i).GetComponent<Image>().sprite = mage;
                        break;
                    case 3:
                        dungeonJournal.transform.GetChild(1).GetChild(i).GetComponent<Image>().sprite = hunter;
                        break;
                }

                if(saveData.mostRecentClasses[i] == 0)
                {
                    dungeonJournal.transform.GetChild(2).GetChild(i).GetComponent<Text>().text = "";
                }
                else
                {
                    dungeonJournal.transform.GetChild(2).GetChild(i).GetComponent<Text>().text = "Level: " + saveData.mostRecentLevels[i] +
                    "\nFloor: " + saveData.mostRecentFloors[i];
                }
            }
            dungeonJournal.transform.GetChild(2).GetChild(5).GetComponent<Text>().text = "Highest Level: " + saveData.highestLevel +
                    "\nHighest Floor: " + saveData.highestFloor;
        }
    }

    public void StartGame()
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene("Main");
    }

    public void PickKnight()
    {

    }

    public void DungeonJournal()
    {
        dungeonJournal.SetActive(!dungeonJournal.activeSelf);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void SettingsToggle()
    {
        if (!settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(true);
            settingsPanel.transform.GetChild(3).gameObject.SetActive(true);
            settingsPanel.transform.GetChild(4).gameObject.SetActive(false);
            settingsPanel.transform.GetChild(5).gameObject.SetActive(false);

            settingsPanel.transform.GetChild(3).GetChild(1).GetComponent<Slider>().value = AudioListener.volume;
        }
        else
        {
            settingsPanel.SetActive(false);
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void ChangeMusicVolume()
    {
        AudioListener.volume = settingsPanel.transform.GetChild(3).GetChild(1).GetComponent<Slider>().value;
    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void ToggleScreensMenu()
    {
        settingsPanel.transform.GetChild(3).gameObject.SetActive(!settingsPanel.transform.GetChild(3).gameObject.activeSelf);
        settingsPanel.transform.GetChild(5).gameObject.SetActive(!settingsPanel.transform.GetChild(5).gameObject.activeSelf);
    }

    public void ToggleControlsMenu()
    {

    }

    public void set1024x576()
    {
        Screen.SetResolution(1024, 576, Screen.fullScreen);
    }

    public void set1280x720()
    {
        Screen.SetResolution(1280, 720, Screen.fullScreen);
    }

    public void set1600x900()
    {
        Screen.SetResolution(1600, 900, Screen.fullScreen);
    }

    public void set1920x1080()
    {
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
    }
}
