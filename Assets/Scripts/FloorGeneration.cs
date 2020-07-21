﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGeneration : MonoBehaviour
{
    public int floor;
    public GameObject[] room1;
    public GameObject[] room2;
    public GameObject[] room3;
    public GameObject[] room4;
    public GameObject[] room5;
    public GameObject[] room6;
    public GameObject[] room7;
    public GameObject[] room8;
    public GameObject[] room9;
    public GameObject[] room10;
    public GameObject[] room11;
    public GameObject[] room12;
    public GameObject[] room13;
    public GameObject[] room14;
    public GameObject[] room15;
    public GameObject[] room16;
    public GameObject[][] prefabs;
    public GameObject[] emptyRooms;

    public GameObject[] rooms;

    public Transform player;
    public Transform stairs;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        stairs = FindObjectOfType<Stairs>().transform;
        prefabs = new GameObject[][]{ room1, room2, room3, room4, room5, room6, room7, room8, room9, room10, room11, room12, room13, room14, room15, room16};
        GenerateFloor(floor);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateFloor(int floor)
    {
        // Player Position
        int randomPlayerPosition = UnityEngine.Random.Range(1,17);
        if(floor == 1)
        {
            player.position = new Vector3(0,0,0);
        }
        else
        {
            float playerX = (((randomPlayerPosition - 1) % 4) * 17);
            float playerY = (float)(((randomPlayerPosition - 1)/4) * -8.5);
            player.position = new Vector3(playerX,playerY,0);
            Camera.main.transform.position = new Vector3(playerX,playerY,Camera.main.transform.position.z);
        }
        PlayerStats.room = randomPlayerPosition;

        // Stair Position
        int randomStairPosition;
        do
        {
            randomStairPosition = UnityEngine.Random.Range(1,17);
        } while(randomStairPosition == randomPlayerPosition);

        float stairX = (((randomStairPosition - 1) % 4) * 17);
        float stairY = (float)(((randomStairPosition - 1)/4) * -8.5);
        stairs.position = new Vector3(stairX,stairY,0);

        for (int i = 1; i <= 16; i++)
        {
            GenerateRoom(i,prefabs[i-1], randomPlayerPosition);
        }
    }

    public void GenerateBossFloor(int boss)
    {
        DestroyFloor();
        player.position = new Vector3(0,0,0);
        Camera.main.transform.position = new Vector3(0,0,Camera.main.transform.position.z);

    }

    void DestroyFloor()
    {
        rooms = GameObject.FindGameObjectsWithTag("Room");
        for(var i = 0; i < rooms.Length; i++)
        {
            Destroy(rooms[i]);
        }
    }

    void GenerateRoom(int room, GameObject[] prefabs, int player)
    {
        GameObject selectedRoom;
        int toSpawn = UnityEngine.Random.Range(0,100);

        if(room == player)
        {
            selectedRoom = emptyRooms[room-1];
        }
        else if(toSpawn < 5)
        {
            selectedRoom = prefabs[0];
        }
        else if(toSpawn < 10)
        {
            selectedRoom = prefabs[1];
        }
        else if(toSpawn < 20)
        {
            selectedRoom = prefabs[2];
        }
        else if(toSpawn < 30)
        {
            selectedRoom = prefabs[3];
        }
        else if(toSpawn < 50)
        {
            selectedRoom = prefabs[4];
        }
        else if(toSpawn < 70)
        {
            selectedRoom = prefabs[5];
        }
        else
        {
            selectedRoom = prefabs[6];
        }

        float x = (((room -1) % 4) * 17);
        float y = (float)(((room - 1)/4) * -8.5);

        Instantiate(selectedRoom, new Vector3(x,y,0), Quaternion.identity);
    }
}
