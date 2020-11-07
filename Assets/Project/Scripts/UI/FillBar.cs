using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FillBar : MonoBehaviour
{
    [SerializeField] private Image fillBar;

    public void SetProgress(int progress, int max)
    {
        fillBar.DOFillAmount((float)progress / max, 1f);
    }
}
