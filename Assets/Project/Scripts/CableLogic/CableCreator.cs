using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class CableCreator : MonoBehaviour
{
    private bool access;

    [SerializeField] private int startLenght = 0;
    [SerializeField] private int sectionNumber = 0;

    [SerializeField] private UnityEvent OnActivate;

    [SerializeField] private GameObject cable;

    IPlayerLogic _playerLogic;

    private void Start()
    {
        _playerLogic = FindObjectsOfType<MonoBehaviour>().OfType<IPlayerLogic>().FirstOrDefault();
    }

    private void Update()
    {
        if (!access)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_playerLogic.GetCable()?.GetSection() + 1 == sectionNumber)
                ThrowCabel();

            CreateCable();
        }
    }

    private void ThrowCabel()
    {
        _playerLogic.GetCable().transform.SetParent(transform);
    }

    private void CreateCable()
    {
        Cable go = Instantiate(cable, _playerLogic.GetPlayerController().transform).GetComponent<Cable>();

        _playerLogic.SetCable(go);
        _playerLogic.GetCable().SetData(startLenght, sectionNumber);

        OnActivate?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            if (!access)
                access = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (access)
                access = false;
        }
    }
}
