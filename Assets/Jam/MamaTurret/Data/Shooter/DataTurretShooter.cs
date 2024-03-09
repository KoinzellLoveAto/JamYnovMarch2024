using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Component/ShooterController")]
public class DataTurretShooter : ScriptableObject
{
    public float AttackSpeed = .7f;

    public float ProjectileForce = 15;
}
