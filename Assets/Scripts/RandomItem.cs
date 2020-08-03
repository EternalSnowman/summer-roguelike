using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    public Items[] toGenerate;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(toGenerate[UnityEngine.Random.Range(0, toGenerate.Length)], 
            new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
