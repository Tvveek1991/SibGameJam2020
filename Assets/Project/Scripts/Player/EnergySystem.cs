using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnergySystem : MonoBehaviour
{
    private const string KEY = "energyDaySave";

    [SerializeField] private UnityEvent OnGameOver;

    private int energy
    {
        get => PlayerPrefs.GetInt(KEY, 100);

        set
        {
            value = Mathf.Clamp(value, 0, 100);
            PlayerPrefs.SetInt(KEY, value);
        }
    }

    private void Start()
    {
        StartCoroutine(LifeTimer());
    }

    IEnumerator LifeTimer()
    {
        yield return new WaitForSecondsRealtime(1f);
        ChangeEnergy(-1);
    }

    public void ChangeEnergy(int numb)
    {
        energy += numb;
    }
}
