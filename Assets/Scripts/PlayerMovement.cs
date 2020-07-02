using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerMovement : MonoBehaviour {
    public Animator animator;
    public static float tempSpeed = 7.0f;
    public static float speed = tempSpeed;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
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

    void OnCollisionEnter2D(Collision2D coll)
    {

    }

    void OnCollisionExit2D(Collision2D coll)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.isTrigger != true && collision.CompareTag("Enemy"))
        {
            collision.SendMessageUpwards("Damage", PlayerStats.STR);
        }

    }

    public void Damage(int damage)
    {
        if(PlayerStats.currentHP >= damage)
        {
            PlayerStats.currentHP -= damage;
        }
        else
        {
            PlayerStats.currentHP = 0;
        }
    }
}
