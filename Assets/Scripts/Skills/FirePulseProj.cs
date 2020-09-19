using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePulseProj : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") && collision.isTrigger != true)
        {
            Destroy(this.gameObject);
            if (collision.isTrigger != true && collision.CompareTag("Enemy"))
            {
                collision.SendMessageUpwards("MagDamage", damage);
            }
        }
    }
}
