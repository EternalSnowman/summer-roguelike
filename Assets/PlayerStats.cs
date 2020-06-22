using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static float HP;
    public static int STR;
    public static int INT;
    public static int AGI;
    public static int DEF;
    public static int RES;

    public Image healthImage;
    public float healthImageWidth;
    public float healthImageHeight;
    public static string direction;

    public Animator animator;
    public bool isAttacking;
    // Start is called before the first frame update
    void Start()
    {
        loadStats();
        direction = "down";
        healthImageWidth = healthImage.rectTransform.rect.width;
        healthImageHeight = healthImage.rectTransform.rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        checkHealth();
        updateDirection();
        if(isAttacking)
        {
            checkAttack();
        }
        healthImage.rectTransform.sizeDelta = new Vector2(Mathf.Lerp(0, healthImageWidth, HP / 100), healthImageHeight);
        healthImage.color = Color.Lerp(Color.red, Color.green, HP / 100);
    }

    void updateDirection()
    {
        switch(direction)
        {
            case "up":
                animator.SetTrigger("upWalk");
                break;
            case "down":
                animator.SetTrigger("downWalk");
                break;
            case "right":
                //animator.SetTrigger("rightIdle");
                break;
            case "left":
                //animator.SetTrigger("leftIdle");
                break;
        }
    }

    void checkAttack()
    {
        if(!animator.GetCurrentAnimatorStateInfo.isName("attack"))
        {
            animator.SetTrigger("attack");
        }
        isAttacking = false;
    }

    void checkHealth()
    {
        if(HP <= 0)
        {
           // SceneManager.LoadScene("GameOver");
        }
    }

    void loadStats()
    {
        HP = 100;
    }
}
