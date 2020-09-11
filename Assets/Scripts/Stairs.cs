﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stairs : MonoBehaviour
{
    public int boss;
    public bool bossFloor;
    public int floor;
    public FloorGeneration generateFloor;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(!col.isTrigger && col.CompareTag("Player"))
        {
            if(bossFloor)
            {
                FloorController.loadTimer = 8f;
                FloorController.loadText = "Floor " + (PlayerStats.floor) + " Boss";
                FloorController.doLoad = true;
                generateFloor.GenerateBossFloor(floor);
                PlayerStats.room = 0;
            }
            else
            {
                if(floor % 5 == 0)
                {
                    for(int i = 0; i < 4; i++)
                    {
                        PlayerStats.miniBossesBeat[i] = false;
                    }
                    if(floor == 10)
                    {
                        PlayerStats.currentHP = 0;
                    }
                }
                FloorController.loadTimer = 8f;
                FloorController.loadText = "Floor " + (PlayerStats.floor + 1);
                FloorController.doLoad = true;
                Instantiate(generateFloor, new Vector3(0, 0, 0), Quaternion.identity);
                PlayerStats.floor += 1;
            }
        }
    }
}
