using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonFire : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") && collision.isTrigger != true)
        {
            Destroy(this.gameObject);

            if (damage - PlayerStats.RES < 0)
            {
                if (collision.isTrigger != true && collision.CompareTag("Player"))
                {
                    collision.SendMessageUpwards("Damage", 1);
                }
            }
            else if (collision.isTrigger != true && collision.CompareTag("Player"))
            {
                collision.SendMessageUpwards("Damage", damage - PlayerStats.RES);
            }
        }
    }
}
