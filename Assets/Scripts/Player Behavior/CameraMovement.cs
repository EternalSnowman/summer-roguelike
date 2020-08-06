using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static bool boss;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(boss)
        {
            Camera.main.transform.position = new Vector3(player.position.x, player.position.y, Camera.main.transform.position.z);
        }
    }
}
