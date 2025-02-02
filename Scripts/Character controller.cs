using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterAnimator : MonoBehaviour
{
    private Animator mAnimator;
    [SerializeField]private Logic input;

    public void Drinking()
    {
        if (input.animatable)
        {
            mAnimator = GetComponent<Animator>();
            mAnimator.enabled = true;
            mAnimator.SetBool("D2", true);
            Debug.Log("true");
            Invoke("NotDrinking", 60*Time.deltaTime);
        }
        else {
            NotDrinking();
            //Debug.Log("Wait");
        }
    }

    private void NotDrinking()
    {
        mAnimator = GetComponent<Animator>();
        mAnimator.enabled = true;
        mAnimator.SetBool("D2", false);
    }
}
