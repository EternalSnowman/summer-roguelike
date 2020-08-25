using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorGeneration : MonoBehaviour
{
    public AudioClip bgm;
    public AudioClip bossMusic;
    public AudioSource musicPrefab;
    public AudioSource musicControl;

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
    public GameObject[] bossRoom;
    public GameObject[][] prefabs;
    public GameObject[] emptyRooms;

    public int floor;

    public GameObject[] rooms;

    public static Transform player;
    public GameObject stairs;

    public GameObject minimap;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] music = GameObject.FindGameObjectsWithTag("Music Control");
        minimap = GameObject.FindGameObjectWithTag("Minimap");
        for (var i = 0; i < music.Length; i++)
        {
            Destroy(music[i]);
        }
        Instantiate(musicPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        musicControl = GameObject.FindGameObjectWithTag("Music Control").GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerStats>().transform;
        prefabs = new GameObject[][]{ room1, room2, room3, room4, room5, room6, room7, room8, room9, room10, room11, room12, room13, room14, room15, room16};
        GenerateFloor();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateFloor()
    {
        musicControl.clip = bgm;
        musicControl.Play();
        DestroyFloor();
        CameraMovement.boss = false;

        for(int i = 0; i < 16; i++)
        {
            minimap.transform.GetChild(i).GetComponent<Image>().color = new Color32(227, 68, 68, 150);
        }

        // Player Position
        int randomPlayerPosition = UnityEngine.Random.Range(1,17);
        float playerX = (((randomPlayerPosition - 1) % 4) * 20);
        float playerY = (float)(((randomPlayerPosition - 1)/4) * -12);
        player.position = new Vector3(playerX,playerY,0);
        Camera.main.transform.position = new Vector3(playerX,playerY,Camera.main.transform.position.z);
        CameraMovement.desiredPosition = Camera.main.transform.position;
        PlayerStats.room = randomPlayerPosition;

        // Stair Position
        int randomStairPosition;
        do
        {
            randomStairPosition = UnityEngine.Random.Range(1,17);
        } while(randomStairPosition == randomPlayerPosition);

        float stairX = (((randomStairPosition - 1) % 4) * 20);
        float stairY = (float)(((randomStairPosition - 1)/4) * -12);
        Instantiate(stairs, new Vector3(stairX,stairY,0), Quaternion.identity);

        for (int i = 1; i <= 16; i++)
        {
            GenerateRoom(i,prefabs[i-1], randomPlayerPosition);
        }
    }

    public void GenerateBossFloor(int boss)
    {
        musicControl = GameObject.FindGameObjectWithTag("Music Control").GetComponent<AudioSource>();
        musicControl.clip = bossMusic;
        musicControl.Play();
        DestroyFloor();

        player.position = new Vector3(0,0,0);
        Camera.main.transform.position = new Vector3(0,0,Camera.main.transform.position.z);
        CameraMovement.desiredPosition = Camera.main.transform.position;
        int randomBossRoom;

        if (boss != 5)
        {
            do
            {
                randomBossRoom = UnityEngine.Random.Range(0, bossRoom.Length);
            }
            while (PlayerStats.miniBossesBeat[randomBossRoom] == true);

            PlayerStats.miniBossesBeat[randomBossRoom] = true;
        }
        else
        {

            do
            {
                randomBossRoom = UnityEngine.Random.Range(0, bossRoom.Length);
            }
            while (PlayerStats.bossesBeat[randomBossRoom] == true);

            PlayerStats.bossesBeat[randomBossRoom] = true;
        }

        Instantiate(bossRoom[randomBossRoom], new Vector3(3.2f,-4,0), Quaternion.identity);
        // use switch on boss to determine which bossRoom to instantiate
        // should have random number
    }

    void DestroyFloor()
    {
        for(int i = 0; i < 8; i++)
        {
            PlayerStats.buffs[i] = PlayerStats.emptyBuff;
        }

        rooms = GameObject.FindGameObjectsWithTag("Room");
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        for (var i = 0; i < rooms.Length; i++)
        {
            Destroy(rooms[i]);
        }
        for (var i = 0; i < items.Length; i++)
        {
            items[i].SetActive(false);
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

        float x = (((room -1) % 4) * 20);
        float y = (float)(((room - 1)/4) * -12);

        Instantiate(selectedRoom, new Vector3(x,y,0), Quaternion.identity);
    }
}
