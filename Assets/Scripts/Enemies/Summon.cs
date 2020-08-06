using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GoblinKing goblinKing = animator.GetComponent<GoblinKing>();
        goblinKing.Summon();
    }
}
