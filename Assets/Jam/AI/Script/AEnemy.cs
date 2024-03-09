using RakaEngine.Controllers.Health;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AEnemy : MonoBehaviour
{
    [field: SerializeField]
    public NavMeshAgent navMeshAgent { get; private set; }



    [field: SerializeField]
    public HealthController healthController { get; private set; }

    [field: SerializeField]
    public Transform Target { get; private set; }

    public Action<AEnemy> OnDeathEnemy;

    public void Awake()
    {
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


}
