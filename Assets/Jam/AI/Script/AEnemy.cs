using RakaEngine.Controllers.Health;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RakaEngine.Statemachine;

public abstract class AEnemy : MonoBehaviour
{
    [field: SerializeField]
    public NavMeshAgent navMeshAgent { get; private set; }

    [field:SerializeField]
    protected MonoStateMachine statemachine;


    [field: SerializeField]
    public HealthController healthController { get; private set; }

    [field: SerializeField]
    public Transform Target { get; private set; }

    public Rigidbody rb { get; private set; }

    public Action<AEnemy> OnDeathEnemy;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        healthController.EventSystem_onDeath += HandleDeathEnemy;
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }
    public void HandleDeathEnemy(HealthController healthController)
    {
        OnDeathEnemy?.Invoke(this);
       
    }

    public abstract void Init(Transform target);

    public virtual void OnPlayerInRange()
    {

    }



}
