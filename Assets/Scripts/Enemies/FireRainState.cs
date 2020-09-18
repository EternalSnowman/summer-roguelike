﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRainState : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Demon demon = animator.GetComponent<Demon>();
        demon.FireRain();
    }
}
