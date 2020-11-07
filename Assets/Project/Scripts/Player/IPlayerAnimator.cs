using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerAnimator
{
    void TakeSmth();

    void Die();

    void OnMove(Vector2 pos);
}
