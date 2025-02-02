/**Script for Button Cooldown Animation */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerIndicator : MonoBehaviour
{
    [SerializeField] private Logic timer;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();  
    }
    private void Update()
    {
        if (timer.animatable)
        {
            if (Math.Abs(timer.ReturnCoolDown()) <= 1) { animator.SetBool("IsCDZero", true); }
        }
        else
        {
            animator.SetBool("IsCDZero", false);
        }
    }
}
