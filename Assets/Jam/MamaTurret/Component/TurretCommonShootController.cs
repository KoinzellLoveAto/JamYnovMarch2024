using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCommonShootController : ATurretShootController
{
    public override void Shoot(Transform from, Vector3 dir)
    {
        if (_canShoot)
        {

            OnShoot?.Invoke();

            ShootFeeback.Play(transform.position);

            _canShoot = false;

            for (int i = 0; i < dataShootController.nbProjectileShoot; i++)
            {
                AAmmo ammo = Instantiate(currentAmmoPrefab, from.position, from.rotation);
                ammo.Shoot(GetImprecisionOnVector(dir,dataShootController.imprecisionMagnitude), dataShootController.ProjectileForce);
            }

            _shootRoutine = StartCoroutine(ShootRoutine());

        }
        else
        {
            //Cannot shoot
        }
    }
}
