using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorController : MonoBehaviour
{
    public FloorGeneration floor1;

    public GameObject loadScreen;
    public static float loadTimer;
    public static string loadText;
    public static bool doLoad;
    
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(floor1, new Vector3(0, 0, 0), Quaternion.identity);
        loadTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(loadTimer > 0)
        {
            Time.timeScale = 0;
            loadTimer -= 0.1f;
            if (doLoad)
            {
                loadScreen.SetActive(true);
                loadScreen.transform.GetChild(0).GetComponent<Text>().text = loadText;
                doLoad = false;
            }
        }
        else
        {
            Time.timeScale = 1;
            loadScreen.SetActive(false);
        }
    }
}
