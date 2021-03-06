﻿using System.Collections;
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
    [SerializeField] private UnityEvent OnThrow;

    [SerializeField] private GameObject cable;

    IPlayerLogic _playerLogic;

    private void Start()
    {
        _playerLogic = FindObjectsOfType<MonoBehaviour>().OfType<IPlayerLogic>().FirstOrDefault();
    }

    private void Update()
    {
        if (!access || sectionNumber < 0)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_playerLogic.GetCable()?.GetSection() + 1 == sectionNumber)
                ThrowCabel();

            if(_playerLogic.GetCable() == null && _playerLogic.GetSection() == sectionNumber)
                CreateCable();
        }
    }

    public void CloseLine()
    {
        sectionNumber = -1;
    }

    private void ThrowCabel()
    {
        OnThrow?.Invoke();

        _playerLogic.GetCable()?.StopAllCoroutines();
        _playerLogic.GetCable()?.transform.SetParent(transform);
        _playerLogic.SetCable(null);

    }

    private void CreateCable()
    {
        Cable go = Instantiate(cable, _playerLogic.GetPlayerController().transform).GetComponent<Cable>();

        _playerLogic.SetCable(go);
        _playerLogic.GetCable().SetData(startLenght, sectionNumber);
        _playerLogic.GetCable().AddListener(_playerLogic.GetPlayerAnimation().Pain);

        OnActivate?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            if (!access)
                access = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (access)
                access = false;
        }
    }
}
