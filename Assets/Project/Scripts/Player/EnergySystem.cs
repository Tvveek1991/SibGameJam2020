using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityEventIntInt : UnityEvent<int, int> { }
public class EnergySystem : MonoBehaviour, IEnergy
{
    private const string KEY = "energySave";

    [SerializeField] private float timeEnergyLost = 0.0f;
    [SerializeField] private UnityEventIntInt OnChangeEnergy = null;
    [SerializeField] private UnityEvent OnEnergyIsOver = null;

    [SerializeField] private int maxEnergy;

    private DeadBot activeBot;
    
    [SerializeField] private Component playerLogic = null;
    private IPlayerLogic _playerLogic;

    private int _energy;
    private int energy
    {
        get => _energy;

        set
        {
            _energy = Mathf.Clamp(value, 0, maxEnergy);
            OnChangeEnergy?.Invoke(_energy, maxEnergy);
            //print(_energy);

            if (_energy == 0)
            {
                print("ENERGY IS OVER");
                OnEnergyIsOver?.Invoke();
            }
        }
    }

    private void Start()
    {
        energy = PlayerPrefs.GetInt(KEY, 6);

        CheckInterfaces();

        StartCoroutine(LifeTimer());
    }
    private void CheckInterfaces()
    {
        _playerLogic = InterfaceTools.GetInteface<IPlayerLogic>(playerLogic);
        if (_playerLogic == null)
            playerLogic = null;
    }

    IEnumerator LifeTimer()
    {
        while (energy != 0)
        {
            yield return new WaitForSecondsRealtime(timeEnergyLost);

            if (_playerLogic.GetInfinityEnergy())
                continue;

            ChangeEnergy(-1);
        }
    }

    public void ChangeEnergy(int numb)
    {
        print("CHANGE ENERGY ON " + numb);
        energy += numb;
    }

    public void TakeEnergy()
    {
        if(activeBot != null)
            activeBot.TakeBattery();
    }

    public void SetDeadBot(DeadBot bot)
    {
        activeBot = bot;
    }

    public int GetEnergy()
    {
        return energy;
    }

    public void SavePoint()
    {
        PlayerPrefs.SetInt(KEY, energy);
    }
}
