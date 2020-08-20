using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabHitbox : MonoBehaviour
{
    public StabSkill damageCalc;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger != true && collision.CompareTag("Enemy"))
        {
            collision.SendMessageUpwards("PhysDamage", damageCalc.damage);
        }
    }
}
