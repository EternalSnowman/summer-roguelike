using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonFireRain : MonoBehaviour
{
    public Animator anim;
    public Collider2D hitbox;
    public int damage;

    public BuffDebuff burning;

    public bool flames;

    private void Start()
    {
        flames = false;
        hitbox.enabled = false;
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Fire Rain"))
        {
            hitbox.enabled = true;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Flames"))
        {
            flames = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") && collision.isTrigger != true)
        {
            if (collision.isTrigger != true && collision.CompareTag("Player"))
            {
                int value = 8;
                for (int i = 0; i < PlayerStats.buffs.Length; i++)
                {
                    if (PlayerStats.buffs[i] == burning)
                    {
                        value = i;
                    }
                }

                if (value < 8)
                {
                    PlayerStats.buffs[value].currDuration = PlayerStats.buffs[value].duration;
                }
                else
                {
                    if (PlayerStats.findNextFree() < 8)
                    {
                        burning.currDuration = burning.duration;
                        burning.Activate();
                        PlayerStats.buffs[PlayerStats.findNextFree()] = burning;
                    }
                }
            }

            if (damage - PlayerStats.RES < 0)
            {
                if (collision.isTrigger != true && collision.CompareTag("Player"))
                {
                    collision.SendMessageUpwards("Damage", 1);
                }
            }
            else if (collision.isTrigger != true && collision.CompareTag("Player"))
            {
                if (flames)
                {
                    collision.SendMessageUpwards("Damage", (damage / 2) - PlayerStats.RES);
                }
                else
                {
                    collision.SendMessageUpwards("Damage", damage - PlayerStats.RES);
                }
            }
        }
    }
}
