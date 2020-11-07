using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour, IPlayerAnimator
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeSmth()
    {
        animator.SetTrigger("Take");
    }

    public void Die()
    {
        animator.SetTrigger("Die");
    }

    public void OnMove(Vector2 pos)
    {
        animator.SetFloat("Horizontal", pos.x);
        animator.SetFloat("Vertical", pos.y);
        animator.SetFloat("Magnitude", pos.magnitude);
    }
}
