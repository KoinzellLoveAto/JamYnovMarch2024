using MoreMountains.Tools;
using RakaEngine.Statemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyKamikaz : AEnemy
{
    public Animator animator;

    private enum kamikazState
    {
        idle,
        chase,
        explode
    }

    private kamikazState state;

    public float DistanceBeforeRush;
    public float RushSpeed = 5;
    public float dammage;
    public float strForcePush;
    public float timeRush;

    public GameObject vxDeath;


    public bool IgnoreState { get; private set; } = false;

    private bool chase = false;
    private bool doOneExplode;
    private Coroutine coroutine;

    public ExplodeTrigger explodeTrigger;
    public override void Init(Transform target)
    {
        SetTarget(target);
    }

    public void DeathFX()
    {
        vxDeath.SetActive(true);
    }
    public override void OnPlayerInRange()
    {
        if (chase == false)
        {
            chase = true;
            animator.SetTrigger("Move");
            state = kamikazState.chase;
        }
    }

    public void Update()
    {
        switch (state)
        {
            case kamikazState.idle:
                break;

            case kamikazState.chase:
                if (Target != null)
                {
                    rb.isKinematic = true;
                    navMeshAgent.SetDestination(Target.position);
                }


                if (Vector3.Distance(transform.position, Target.position) <= DistanceBeforeRush)
                {
                    state = kamikazState.explode;
                    return;
                }
                break;

            case kamikazState.explode:
                if (doOneExplode == false)
                {

                    Explode();
                    doOneExplode = true;
                }
                break;
        }

    }
    public void Explode()
    {
        animator.SetTrigger("isAttacking");
        navMeshAgent.isStopped = true;
        rb.isKinematic = false;
        Vector3 directionToPlayer = (Target.position - transform.position).normalized;
        rb.velocity = directionToPlayer * RushSpeed;
        coroutine = StartCoroutine(DelayKillSelf());
    }

    public IEnumerator DelayKillSelf()
    {

        yield return new WaitForSeconds(timeRush);
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        DeathFX();
        explodeTrigger.onDammageableHit += OnPlayerTouch;


        yield return new WaitForSeconds(0.75f);
        explodeTrigger.onDammageableHit -= OnPlayerTouch;
        healthController.Dammage(9999);

    }

    public void OnPlayerTouch(IDamageable character)
    {

        StopAllCoroutines();
        StartCoroutine(DelayKillSelf());
        character.Damage(dammage);
        healthController.Dammage(9999);

    }
}

