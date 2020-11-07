using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IPlayerLogic
{
    PlayerController GetPlayerController();
    void SetInfinityEnergy(bool value);
    bool GetInfinityEnergy();
    void SetByBattery(bool value);
    bool GetByBattery();
    void SetCable(Cable value);
    Cable GetCable();
}

public class PlayerLogic : MonoBehaviour, IPlayerLogic
{

    [SerializeField] private PlayerController player;
    
    //Нахождение рядом со сломаным ботом
    private bool byBattery;
    //Бесконечная энергия
    private bool isInfinityEnergy;

    private Cable cable;

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

    public void SetCable(Cable value)
    {
        cable = value;
    }
    public Cable GetCable()
    {
        return cable;
    }

    public PlayerController GetPlayerController()
    {
        return player;
    }
}
