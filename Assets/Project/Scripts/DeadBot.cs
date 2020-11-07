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

    private void Start()
    {
        _energy = FindObjectsOfType<MonoBehaviour>().OfType<IEnergy>().FirstOrDefault();
    }

    public void TakeBattery()
    {
        if (isAlive)
        {
            isAlive = !isAlive;
            _energy.ChangeEnergy(energyBoost);
        }
        else
        {
            OnBatteryOff?.Invoke();
            print("Place is clear");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (!isAlive)
                return;

            _energy.SetDeadBot(this);
            collision.GetComponent<PlayerController>().SetByBattery(true);
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
            collision.GetComponent<PlayerController>().SetByBattery(false);
        }
    }
}
