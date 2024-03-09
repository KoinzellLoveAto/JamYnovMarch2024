using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCommonShootController : ATurretShootController
{
    public override void Shoot(Transform from, Vector3 dir)
    {
        AAmmo ammo =  Instantiate(currentAmmoPrefab, from);


    }
}
