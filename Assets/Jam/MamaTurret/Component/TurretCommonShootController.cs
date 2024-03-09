using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCommonShootController : ATurretShootController
{
    public override void Shoot(Transform from, Vector3 dir)
    {
        OnShoot?.Invoke();

        AAmmo ammo =  Instantiate(currentAmmoPrefab, from.position, from.rotation);
        ammo.Shoot(dir, dataShootController.ProjectileForce);
    }
}
