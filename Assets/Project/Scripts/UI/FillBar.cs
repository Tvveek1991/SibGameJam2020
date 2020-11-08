using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FillBar : MonoBehaviour
{
    [SerializeField] private Text textBattery;
    [SerializeField] private Text textLenght;

    public void SetProgress(int progress, int max)
    {
        textBattery.text = $"Батарейка = {(int)(((float)progress / max) * 100)}";
    }
    public void SetProgressLenght(float lenght)
    {
        textLenght.text = $"Кабель = {lenght}";
    }
}
