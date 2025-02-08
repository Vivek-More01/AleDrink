/**Script for allowing button clicks. I will remove this one soon and merge it with TimeIndicator script.*/

using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Logic pushable;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (pushable.animatable)
        {
            animator.SetBool("pushable", true);
        }
        else 
        {
            animator.SetBool("pushable", false);
        }
    }
}
