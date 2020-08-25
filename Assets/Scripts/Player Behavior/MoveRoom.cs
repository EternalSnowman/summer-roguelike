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

    public float smoothSpeed = 1f;
    public static Vector3 desiredPosition;

    // Start is called before the first frame update
    void Start()
    {
        musicControl = GameObject.FindGameObjectWithTag("Music Control").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
    }

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
            desiredPosition = new Vector3(desiredPosition.x + x, desiredPosition.y + y, desiredPosition.z);
            collision.transform.position += playerChange;
            PlayerStats.room += roomChange;
        }
    }
}
