using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AEnemy : MonoBehaviour
{
    [field:SerializeField]
    public NavMeshAgent navMeshAgent { get; private set; }
}
