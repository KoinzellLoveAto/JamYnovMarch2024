using MoreMountains.Tools;
using RakaEngine.Controllers.Health;
using RakaEngine.Statemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.GridLayoutGroup;

public class ExplodeTest : AMonoState
{
    public EnemyKamikaz owner;
    public Coroutine coroutine;

    public ExplodeTrigger explodeTrigger;
    public override void OnEnter()
    {
        base.OnEnter();
        explodeTrigger.onDammageableHit += OnPlayerTouch;
        owner.navMeshAgent.isStopped = true;
        owner.rb.isKinematic = false;
        Vector3 directionToPlayer = (owner.Target.position - transform.position).normalized;
        owner.rb.velocity = directionToPlayer * owner.RushSpeed;
        coroutine =  StartCoroutine(DelayKillSelf());
    }

    public IEnumerator DelayKillSelf()
    {

        yield return new WaitForSeconds(owner.timeRush);
        owner.rb.velocity = Vector3.zero;
        owner.rb.isKinematic = true;
        owner.DeathFX();

        yield return new WaitForSeconds(5);
        owner.healthController.Dammage(9999);

    }
    public IEnumerator DelayBeforeDestroySelf()
    {
        owner.DeathFX();
        yield return new WaitForSeconds(2f);
        owner.healthController.Dammage(9999);

    }
    public void OnPlayerTouch(IDamageable character)
    {
        
        //StopAllCoroutines();
        //StartCoroutine(DelayBeforeDestroySelf());
        explodeTrigger.onDammageableHit -= OnPlayerTouch;
        character.Damage(owner.dammage);
        owner.healthController.Dammage(9999);

    }
}
