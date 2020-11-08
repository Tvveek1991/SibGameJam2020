using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IPlayerLogic
{
    PlayerController GetPlayerController();
    PlayerAnimation GetPlayerAnimation();
    void SetInfinityEnergy(bool value);
    bool GetInfinityEnergy();
    void SetByBattery(bool value);
    bool GetByBattery();
    void SetCable(Cable value);
    Cable GetCable();
    void AddSection();
    void ClearSection();
    int GetSection();
}

public class PlayerLogic : MonoBehaviour, IPlayerLogic
{

    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerAnimation anim;
    
    //Нахождение рядом со сломаным ботом
    private bool byBattery;
    //Бесконечная энергия
    private bool isInfinityEnergy;

    private int activeSection = 1;

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

    public PlayerAnimation GetPlayerAnimation()
    {
        return anim;
    }

    public void AddSection()
    {
        activeSection++;
    }

    public void ClearSection()
    {
        activeSection = 1;
    }

    public int GetSection()
    {
        return activeSection;
    }
}
