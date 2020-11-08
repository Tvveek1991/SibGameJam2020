using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimation : MonoBehaviour, IPlayerAnimator
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private Transform trPlayer;

    private PlayerController player;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    public void TakeSmth()
    {
        animator.SetTrigger("Take");
        StartCoroutine(waitUp(1.8f));
    }

    public void Die()
    {
        animator.SetTrigger("Die");
    }

    public void Pain()
    {
        animator.SetTrigger("Pain");
        StartCoroutine(waitUp(.8f));
    }

    public void OnMove(Vector2 pos)
    {
        animator.SetFloat("Horizontal", pos.x);
        animator.SetFloat("Vertical", pos.y);
        animator.SetFloat("Magnitude", pos.magnitude);

        trPlayer.rotation = Quaternion.Euler(0, pos.x > 0 ? 180 : 0, 0);
    }

    IEnumerator waitUp(float time)
    {
        player.SetMoveAccess(false);
        yield return new WaitForSecondsRealtime(time);
        player.SetMoveAccess(true);
    }
}
