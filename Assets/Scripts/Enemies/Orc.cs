using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : Enemy
{

    public bool attackAnim = false;

    public override void LoadStats()
    {
        LVL = 1;
        maxHP = 100 + (LVL*30);
        currentHP = maxHP;

        STR = 30 + ((LVL-1) * 7);
        INT = 0;
        AGI = 1;
        DEF = 10 + ((LVL-1) * 5);
        RES = LVL/2;

        baseExpYield = 250 + ((LVL-1)*10);

        tempSpeed = 1.5f;
        speed = tempSpeed;
        ATKRNG = 2f;
        tempAttackCD = 1f;
        attackCD = tempAttackCD;
    }

    public override void AttackCheck()
    {
        // Toggle isAttacking
        if(!flashActive)
        {
            if (((Vector2.Distance(target.position, transform.position) <= ATKRNG) || attackAnim) && (attackCD <= 0f))
            {
                isAttacking = true;
                attackCD = tempAttackCD;
                enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

            }
            else
            {
                isAttacking = false;
                enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        // Keep Orc from moving during attack animation
        if( anim.GetCurrentAnimatorStateInfo(0).IsName("downIdleAttack1") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("downIdleAttack2") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("upIdleAttack1") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("upIdleAttack2") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("leftIdleAttack1") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("leftIdleAttack2") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("rightIdleAttack1") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("rightIdleAttack2"))
        {
            attackAnim = true;
        }
        else{
            attackAnim = false;
        }

        // Toggle off next frame
        anim.SetBool("Attacking", isAttacking);

        // Toggle directional attack booleans
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("downIdleAttack2"))
        {
            downAttack.enabled = true;
        }
        else
        {
            downAttack.enabled = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("leftIdleAttack2"))
        {
            leftAttack.enabled = true;
        }
        else
        {
            leftAttack.enabled = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("upIdleAttack2"))
        {
            upAttack.enabled = true;
        }
        else
        {
            upAttack.enabled = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("rightIdleAttack2"))
        {
            rightAttack.enabled = true;
        }
        else
        {
            rightAttack.enabled = false;
        }

        if ((downAttack.enabled == false) && (upAttack.enabled == false) && (rightAttack.enabled == false) && (leftAttack.enabled == false) && !flashActive)
        {
            enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

    }
}
