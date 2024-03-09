using RakaEngine.Controllers.Health;
using RakaEngine.Statemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ExplodeTest : AMonoState
{
    public EnemyKamikaz owner;

    public override void OnEnter()
    {
        base.OnEnter();
        owner.navMeshAgent.isStopped = true;
        owner.rb.isKinematic = false;
        Vector3 directionToPlayer = (owner.Target.position - transform.position).normalized;
        owner.rb.velocity = directionToPlayer * owner.RushSpeed;
    }

    public void OnPlayerTouch(Character character)
    {
        character.healthController.Dammage(owner.dammage);
    }
}
