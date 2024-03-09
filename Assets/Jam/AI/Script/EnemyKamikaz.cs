using RakaEngine.Statemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyKamikaz : AEnemy
{

    public float DistanceBeforeRush;
    public float RushSpeed = 5;
    public float dammage;
    public float strForcePush;

    public AMonoState InitState;
    public AMonoState ChaseState;
    public override void Init(Transform target)
    {
        statemachine.Initialize();
        SetTarget(target);
    }

    public override void OnPlayerInRange()
    {
        base.OnPlayerInRange();
        statemachine.ChangeCurrentState(ChaseState);
    }

}
