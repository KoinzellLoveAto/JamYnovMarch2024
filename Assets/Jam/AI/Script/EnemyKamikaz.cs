using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyKamikaz : AEnemy
{
    public override void Init()
    {
        statemachine.ChangeCurrentState(new StateIdle());
    }

    public override void OnPlayerInRange()
    {
        base.OnPlayerInRange();
        statemachine.ChangeCurrentState(new ChaseState());

    }

}
