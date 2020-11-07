using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnergy
{
    void ChangeEnergy(int numb);

    int GetEnergy();

    void TakeEnergy();

    void SetDeadBot(DeadBot bot);
}
