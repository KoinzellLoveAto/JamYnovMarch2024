using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Data", menuName = "Component/ShooterController")]
public class DataTurretShooter : ScriptableObject
{
    public float DelayBetweenShoot = .7f;

    public Vector2 ProjectileForceRange = new Vector2(15,20);

    public float ProjectileForce => Random.Range(ProjectileForceRange.x,ProjectileForceRange.y);

    public float imprecisionMagnitude =0;
    public int nbProjectileShoot;



}
