using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static float maxHP;
    public static float currentHP;
    public static int STR;
    public static int INT;
    public static int AGI;
    public static int DEF;
    public static int RES;

    public Image healthImage;
    public float healthImageWidth;
    public float healthImageHeight;
    //public static string direction;



    // Start is called before the first frame update
    void Start()
    {
        maxHP = 100;
        loadStats();
        //direction = "down";
        healthImageWidth = healthImage.rectTransform.rect.width;
        healthImageHeight = healthImage.rectTransform.rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        checkHealth();

        healthImage.rectTransform.sizeDelta = new Vector2(Mathf.Lerp(0, healthImageWidth, currentHP / maxHP), healthImageHeight);
        healthImage.color = Color.Lerp(Color.red, Color.green, currentHP / maxHP);
    }

/*
    void checkAttack()
    {
        if(!animator.GetCurrentAnimatorStateInfo().isName("attack"))
        {
            animator.SetTrigger("attack");
        }
        isAttacking = false;
    }
*/
    void checkHealth()
    {
        if(currentHP <= 0)
        {
           // SceneManager.LoadScene("GameOver");
        }
    }

    void loadStats()
    {
        currentHP = maxHP;
        STR = 20;
    }
}
