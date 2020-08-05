using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuff : MonoBehaviour
{
    public float duration;
    public float currDuration;
    public SpriteRenderer icon;
    public bool active;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currDuration -= Time.deltaTime;
        if (currDuration <= 0 && active)
        {
            Deactivate();
        }
    }

    public virtual void Activate()
    {
        active = true;
    }

    public virtual void Deactivate()
    {
        active = false;

        int shift = 0;

        for(int i = 0; i < PlayerStats.buffs.Length; i++)
        {
            if (PlayerStats.buffs[i] == this)
            {
                PlayerStats.buffs[i] = PlayerStats.emptyBuff;
                shift = i;
            }
        }

        for(int i = shift; i < PlayerStats.buffs.Length - 1; i++)
        {
            PlayerStats.buffs[i] = PlayerStats.buffs[i + 1];
        }
        
    }
}
