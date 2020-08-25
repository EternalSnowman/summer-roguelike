using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public float lockout;
    public float tempLockout;
    public GameObject settingsPanel;
    public float debug;
    // Start is called before the first frame update
    void Start()
    {
        tempLockout = 1f;
        lockout = 0f;

        settingsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(lockout > 0)
        {
            // based on computer/game fps (could be an issue in the future)
            lockout -= 0.01f;
        }

        if (Input.GetKey(KeyCode.Escape) && lockout <= 0)
        {
            lockout = tempLockout;
            if(Time.timeScale != 0)
            {
                settingsPanel.SetActive(true);
                settingsPanel.transform.GetChild(3).gameObject.SetActive(true);
                settingsPanel.transform.GetChild(4).gameObject.SetActive(false);
                settingsPanel.transform.GetChild(5).gameObject.SetActive(false);

                settingsPanel.transform.GetChild(3).GetChild(1).GetComponent<Slider>().value = AudioListener.volume;

                Time.timeScale = 0;

            }
            else
            {
                settingsPanel.SetActive(false);
                Time.timeScale = 1;
            }
        }
        debug = Time.timeScale;
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
        settingsPanel.transform.GetChild(3).gameObject.SetActive(!settingsPanel.transform.GetChild(3).gameObject.activeSelf);
        settingsPanel.transform.GetChild(4).gameObject.SetActive(!settingsPanel.transform.GetChild(4).gameObject.activeSelf);
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
