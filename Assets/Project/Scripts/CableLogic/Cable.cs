using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    private const string KEY = "cableLenght";

    [SerializeField] private int startLenght = 0;
    private int cableLenght;
    private int _cableLenght
    {
        get => cableLenght;
        set
        {
            cableLenght = value;
        }
    }

    private void Start()
    {
        cableLenght = PlayerPrefs.GetInt(KEY, startLenght);
    }
}
