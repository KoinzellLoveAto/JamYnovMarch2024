using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Data", menuName = "Component/ShooterController")]
public class DataTurretShooter : ScriptableObject
{
    public float DelayBetweenShoot = .7f;

    public float ProjectileForce = 15;


    public float imprecisionMagnitude =0;
    public int nbProjectileShoot;



}
