using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerMovement : MonoBehaviour {
    public Animator animator;
    public static float tempSpeed = 5f;
    public static float speed = tempSpeed;

    public bool flashActive;
    public float flashLength = 0f;
    public float flashCounter = 0f;

    public SpriteRenderer playerSprite;
    public float red;
    public float green;
    public float blue;

    // Use this for initialization
    void Start () {
        playerSprite = GetComponent<SpriteRenderer>();
        red = playerSprite.color.r;
        green = playerSprite.color.g;
        blue = playerSprite.color.b;
    }

    // Update is called once per frame
    void Update () {
        if(flashActive)
        {
            if(flashCounter > flashLength * .99f)
            {
                playerSprite.color = new Color(red,green,blue, .1f);
            }
            else if (flashCounter > flashLength * .82f)
            {
                playerSprite.color = new Color(red,green,blue, 1f);
            }
            else if(flashCounter > flashLength * .66f)
            {
                playerSprite.color = new Color(red,green,blue, .1f);
            }
            else if (flashCounter > flashLength * .49f)
            {
                playerSprite.color = new Color(red,green,blue, 1f);
            }
            else if(flashCounter > flashLength * .33f)
            {
                playerSprite.color = new Color(red,green,blue, .1f);
            }
            else if (flashCounter > flashLength * .16f)
            {
                playerSprite.color = new Color(red,green,blue, 1f);
            }
            else if(flashCounter > 0f)
            {
                playerSprite.color = new Color(red,green,blue, .1f);
            }
            else
            {
                playerSprite.color = new Color(red,green,blue, 1f);
                flashActive = false;
            }
            flashCounter -= Time.deltaTime;

        }
        movementSpeed();
        readKeys();
    }

    void readKeys()
    {
        float movex = Input.GetAxis("Horizontal");
        float movey = Input.GetAxis("Vertical");
        GetComponent<Rigidbody2D>().velocity = new Vector2(movex * speed, movey * speed);
        GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Extrapolate;


        animator.SetFloat("Horizontal", movex);
        animator.SetFloat("Vertical", movey);


    }

    void movementSpeed(){
        speed = (tempSpeed/2) + ((PlayerStats.currentHP / PlayerStats.maxHP) * (tempSpeed / 2));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.isTrigger != true && collision.CompareTag("Enemy"))
        {
            collision.SendMessageUpwards("PhysDamage", PlayerStats.STR);
        }

    }

    public void Damage(int damage)
    {
        if(!flashActive)
        {
            if(PlayerStats.currentHP >= damage)
            {
                PlayerStats.currentHP -= damage;
            }
            else
            {
                PlayerStats.currentHP = 0;
            }
            flashActive = true;
            flashCounter = flashLength;
        }


    }
}
