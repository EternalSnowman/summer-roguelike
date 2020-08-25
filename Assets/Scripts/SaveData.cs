using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int[] mostRecentLevels;
    public int[] mostRecentFloors;
    public int[] mostRecentClasses;
    // 0 = no run, 1 = knight, 2 = mage, 3 = hunter

    public bool[] seenEnemies; // TODO: make a seenEnemy (on hit)
    public int highestLevel;
    public int highestFloor;

    public float musicVolume;
    public bool fullScreen;
    public int resolution;

    public SaveData()
    {
        mostRecentClasses = new int[5];
        mostRecentFloors = new int[5];
        mostRecentLevels = new int[5];

        seenEnemies = new bool[10]; // CHANGE AS WE GET MORE ENEMIES
        highestLevel = 0;
        highestFloor = 0;

        musicVolume = 1;
        fullScreen = false;
        resolution = 2;
    }

    public SaveData(SaveData prev)
    {
        mostRecentClasses = new int[5];
        mostRecentFloors = new int[5];
        mostRecentLevels = new int[5];

        for (int i = 1; i < 5; i++)
        {
            mostRecentClasses[i] = prev.mostRecentClasses[i - 1];
            mostRecentFloors[i] = prev.mostRecentFloors[i - 1];
            mostRecentLevels[i] = prev.mostRecentLevels[i - 1];
        }

        mostRecentClasses[0] = 1;
        mostRecentLevels[0] = PlayerStats.LVL;
        mostRecentFloors[0] = PlayerStats.floor;

        seenEnemies = prev.seenEnemies; // TODO: come back to this

        for(int i = 0; i < 10; i++)
        {
            if (PlayerStats.seenEnemies[i])
            {
                seenEnemies[i] = true;
            }
        }

        highestLevel = (PlayerStats.LVL > prev.highestLevel) ? PlayerStats.LVL : prev.highestLevel;
        highestFloor = (PlayerStats.floor > prev.highestFloor) ? PlayerStats.floor : prev.highestFloor;

        musicVolume = AudioListener.volume;
        fullScreen = Screen.fullScreen;
        if(Screen.height == 576)
        {
            resolution = 1;
        }
        else if(Screen.height == 900)
        {
            resolution = 3;
        }
        else if(Screen.height == 1080)
        {
            resolution = 4;
        }
        else
        {
            resolution = 2;
        }
    }


}
