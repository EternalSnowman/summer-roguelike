using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : Enemy
{
    public GameObject[] enemies;
    public float skillCD;
    public int cycle;
    public bool attackAnim;
    public GameObject goblin;
    public bool transition = true;
    public bool Phase1 = true;
    
    public Slider healthBar;
    public int miniBossNumber;

    public GameObject bossRewards;

    // Start is called before the first frame update
    void Start()
    {
        LoadStats();
        enemySprite = GetComponent<SpriteRenderer>();
        enemyRigidBody = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerStats>().transform;
        upAttack.enabled = false;
        rightAttack.enabled = false;
        leftAttack.enabled = false;
        downAttack.enabled = false;
        healthBar = GameObject.FindGameObjectWithTag("Boss Health").GetComponent<Slider>();

        bossRewards.SetActive(false);
    }
    

    public void CheckStatus()
    {
        healthBar.value = currentHP;
        healthBar.maxValue = maxHP;
    }

    public override void HandleDeath()
    {
        if(currentHP <= 0){
            PlayerStats.EXP += baseExpYield;
            bossRewards.SetActive(true);
            for (var i = 0; i < enemies.Length; i++)
            {
                GameObject.Destroy(enemies[i]);
            }
        }
    }
}
