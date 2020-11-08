using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LineBuilder : MonoBehaviour
{
    private const string KEY = "line_";
    [SerializeField] private int lineNumber;

    [SerializeField] private GameObject trail;
    [SerializeField] private List<Transform> staitions;
    private CableCreator firstCableCreator;

    private bool isOver
    {
        get => PlayerPrefs.GetInt(KEY + lineNumber, 0) == 1 ? true : false;
        set
        {
            PlayerPrefs.SetInt(KEY + lineNumber, value ? 1 : 0);

            if(value)
                firstCableCreator.CloseLine();
        }
    }

    private void Awake()
    {
        firstCableCreator = staitions[0].GetComponent<CableCreator>();

        if (isOver)
            MakeLines();
    }

    public void SetIsOver(bool value)
    {
        isOver = value;
    }

    public bool GetIsOver()
    {
        return isOver;
    }

    private void MakeLines()
    {
        for (int i = 0; i < staitions.Count - 1; i++)
        {
            Transform trTrail = Instantiate(trail, staitions[i].position, Quaternion.identity).transform;
            trTrail.DOMove(staitions[i + 1].position, 0.25f);
        }
    }
}
