using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    public int boss;
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
            generateFloor.GenerateBossFloor(boss);
        }
    }
}
