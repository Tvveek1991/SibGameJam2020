using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class DeadBot : MonoBehaviour
{
    [SerializeField] private bool isAlive = true;

    [SerializeField] private int energyBoost;

    [SerializeField] private UnityEvent OnBatteryOff;
    [SerializeField] private UnityEvent OnVisualContact;

    IEnergy _energy;
    IPlayerLogic _playerLogic;

    [SerializeField] private Animator animator = null;

    private void Start()
    {
        _energy = FindObjectsOfType<MonoBehaviour>().OfType<IEnergy>().FirstOrDefault();
        _playerLogic = FindObjectsOfType<MonoBehaviour>().OfType<IPlayerLogic>().FirstOrDefault();
    }

    public void TakeBattery()
    {
        if (isAlive)
        {
            isAlive = !isAlive;
            _energy.ChangeEnergy(energyBoost);
            Die();
        }
        else
        {
            OnBatteryOff?.Invoke();
            print("Place is clear");
        }
    }

    public void Die()
    {
        animator.SetTrigger("Die");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (!isAlive)
                return;

            _energy.SetDeadBot(this);
            _playerLogic.SetByBattery(true);
            OnVisualContact?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (!isAlive)
                return;

            _energy.SetDeadBot(null);
            _playerLogic.SetByBattery(false);
        }
    }
}
