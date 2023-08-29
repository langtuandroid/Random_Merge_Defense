using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAnimationSystem
{
    Animator animator;
    string attackId = "Attack";
    public TowerAnimationSystem(Animator animator)
    {
        this.animator = animator;
    }
    public void AttackPlay()
    {
        animator.SetTrigger(attackId);
    }

}
