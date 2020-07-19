using System;
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

    // Start is called before the first frame update
    void Start()
    {
        // spawn player and stairs before rooms, set those rooms as empty
        prefabs = new GameObject[][]{ room1, room2, room3, room4, room5, room6, room7, room8, room9, room10, room11, room12, room13, room14, room15, room16};
        GenerateFloor(floor);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateFloor(int floor)
    {
        for (int i = 1; i <= 16; i++)
        {
            GenerateRoom(i,prefabs[i-1]);
        }
    }

    void GenerateRoom(int room, GameObject[] prefabs)
    {
        GameObject selectedRoom;
        int toSpawn = UnityEngine.Random.Range(0,100);

        if(toSpawn < 5)
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

        float x = (((room -1) % 4)) * 17;
        float y = (float)(((room - 1)/4)*-8.5);

        Instantiate(selectedRoom, new Vector3(x,y,0), Quaternion.identity);
    }
}
