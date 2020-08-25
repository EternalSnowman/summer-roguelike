using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static bool boss;
    public Transform player;
    public static Vector3 desiredPosition;
    public float smoothSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        desiredPosition = Camera.main.transform.position;
        smoothSpeed = 15f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(boss)
        {
            Camera.main.transform.position = new Vector3(player.position.x, player.position.y, Camera.main.transform.position.z);
        }
        else
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        }
    }
}
