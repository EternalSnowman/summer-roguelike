using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfoButton : MonoBehaviour
{
    public int enemyID;
    public EnemyInfo infoRef;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getTooltip()
    {
        infoRef.tooltip.SetActive(!infoRef.tooltip.activeSelf);

        if (infoRef.tooltip.activeSelf){
            infoRef.UpdateUI(enemyID);
        }
    }
}
