using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public static bool isAttacking;
    public Animator anim;
    public Collider2D upAttack;
    public Collider2D rightAttack;
    public Collider2D leftAttack;
    public Collider2D downAttack;

    // Start is called before the first frame update
    void Start()
    {
        upAttack.enabled = false;
        rightAttack.enabled = false;
        leftAttack.enabled = false;
        downAttack.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       // Toggle isAttacking
       if (Input.GetKey(KeyCode.J) && !isAttacking){
           isAttacking = true;

       }
       // Toggle off next frame
       else{
            isAttacking = false;
       }

       // Toggle directional attack booleans
       if(anim.GetCurrentAnimatorStateInfo(0).IsName("upIdleAttack") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("upWalkAttack"))
       {
            upAttack.enabled = true;
       }
       else
       {
            upAttack.enabled = false;
       }
       if(anim.GetCurrentAnimatorStateInfo(0).IsName("rightIdleAttack") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("rightWalkAttack"))
       {
            rightAttack.enabled = true;
       }
       else
       {
            rightAttack.enabled = false;
       }
       if(anim.GetCurrentAnimatorStateInfo(0).IsName("leftIdleAttack") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("leftWalkAttack"))
       {
            leftAttack.enabled = true;
       }
       else
       {
            leftAttack.enabled = false;
       }
       if(anim.GetCurrentAnimatorStateInfo(0).IsName("downIdleAttack") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("downWalkAttack"))
       {
            downAttack.enabled = true;
       }
       else
       {
            downAttack.enabled = false;
       }


       anim.SetBool("Attacking", isAttacking);
    }
}
