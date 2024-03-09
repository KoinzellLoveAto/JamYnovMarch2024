using RakaEngine.Statemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : AMonoState
{
   public  EnemyKamikaz owner;

    public ExplodeTest explodeState;

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (owner.Target != null)
        {
           owner.navMeshAgent.SetDestination(owner.Target.position);
        }


        if (Vector3.Distance(owner.transform.position, owner.Target.position) <= owner.DistanceBeforeRush)
        {
            RequestChangeState(explodeState);
        }

    }
}
