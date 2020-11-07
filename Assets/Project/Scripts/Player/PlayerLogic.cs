using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IPlayerLogic
{
    void SetInfinityEnergy(bool value);
    bool GetInfinityEnergy();
    void SetByBattery(bool value);
    bool GetByBattery();
}

public class PlayerLogic : MonoBehaviour, IPlayerLogic
{

    [SerializeField] private PlayerController player;
    
    //Нахождение рядом со сломаным ботом
    private bool byBattery;
    //В руках есть кабель
    private bool isCableOn;
    //Бесконечная энергия
    private bool isInfinityEnergy;

    public bool GetByBattery()
    {
        return byBattery;
    }

    public void SetByBattery(bool value)
    {
        byBattery = value;
    }

    public void SetInfinityEnergy(bool value)
    {
        isInfinityEnergy = value;
    }

    public bool GetInfinityEnergy()
    {
        return isInfinityEnergy;
    }
}
