using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    public FloorGeneration floor1;
    public FloorGeneration floor2;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(floor1, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
