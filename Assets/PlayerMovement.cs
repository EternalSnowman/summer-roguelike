using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour {
    public float speed = 15.0f;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        readKeys();
    }

    void readKeys()
    {
        float movex = Input.GetAxis("Horizontal");
        float movey = Input.GetAxis("Vertical");
        GetComponent<Rigidbody2D>().velocity = new Vector2(movex * speed, movey * speed);
        GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Extrapolate;

        // Change Direction based on key input
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
            PlayerStats.direction = "up";
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
            PlayerStats.direction = "down";
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            PlayerStats.direction = "right";
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            PlayerStats.direction = "left";
        }

        if (Input.GetKey(KeyCode.J)){
            PlayerStats.isAttacking = true;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

    }

    void OnCollisionExit2D(Collision2D coll)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats.HP -= 10;
    }
}
