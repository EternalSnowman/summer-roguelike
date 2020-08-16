using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                generateFloor.GenerateBossFloor(floor);
                PlayerStats.room = 0;
            }
            else
            {
                if(floor == 5)
                {
                    SceneManager.LoadScene("GameOver");
                }
                Instantiate(generateFloor, new Vector3(0, 0, 0), Quaternion.identity);
                PlayerStats.floor += 1;
            }
        }
    }
}
