using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour, IPlayerAnimator
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private Transform trPlayer;

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

        trPlayer.rotation = Quaternion.Euler(0, pos.x > 0 ? 180 : 0, 0);
    }
}
