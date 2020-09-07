using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRoom : MonoBehaviour
{
    public Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for(int i = 0; i < enemies.Length; i++)
            {
                if(enemies[i].GetComponent<Enemy>().room == PlayerStats.room)
                {
                    enemies[i].GetComponent<Animator>().SetBool("Stationary", true);
                }
            }
            PlayerStats.room += 20;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<Animator>().SetBool("Stationary", false);
            }

            PlayerStats.room -= 20;
        }
    }
}
