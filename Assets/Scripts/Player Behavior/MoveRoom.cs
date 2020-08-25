using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRoom : MonoBehaviour
{
    public float x;
    public float y;
    public Vector3 playerChange;
    public int roomChange;

    public AudioClip bossMusic;
    public AudioSource musicControl;

    // Start is called before the first frame update
    void Start()
    {
        musicControl = GameObject.FindGameObjectWithTag("Music Control").GetComponent<AudioSource>();
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(PlayerStats.room == 0)
            {
                CameraMovement.boss = true;
                musicControl.clip = bossMusic;
                musicControl.Play();
            }
            CameraMovement.desiredPosition = new Vector3(CameraMovement.desiredPosition.x + x, CameraMovement.desiredPosition.y + y, CameraMovement.desiredPosition.z);
            collision.transform.position += playerChange;
            PlayerStats.room += roomChange;
        }
    }
}
