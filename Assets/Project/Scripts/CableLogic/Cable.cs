using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    private int cableLenght;
    private int sectionNumber;

    public void SetData(int lenght, int section)
    {
        cableLenght = lenght;
        sectionNumber = section;
    }

    public int GetSection()
    {
        return sectionNumber;
    }
}
