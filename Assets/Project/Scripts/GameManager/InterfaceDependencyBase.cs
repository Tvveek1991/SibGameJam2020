using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceDependencyBase : MonoBehaviour
{
    [SerializeField] private Component energy;
    protected IEnergy _energy;

    [SerializeField] private Component playerAnim = null;
    protected IPlayerAnimator _playerAnim;

    [SerializeField] private Component playerLogic = null;
    protected IPlayerLogic _playerLogic;

    private void Start()
    {
        CheckInterfaces();
    }

    protected void CheckInterfaces()
    {
        _energy = InterfaceTools.GetInteface<IEnergy>(energy);
        if (_energy == null)
            energy = null;

        _playerAnim = InterfaceTools.GetInteface<IPlayerAnimator>(playerAnim);
        if (_playerAnim == null)
            playerAnim = null;

        _playerLogic = InterfaceTools.GetInteface<IPlayerLogic>(playerLogic);
        if (_playerLogic == null)
            playerLogic = null;
    }
}
