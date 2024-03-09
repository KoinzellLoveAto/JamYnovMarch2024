using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [field: SerializeField]
    public TurretHead turretHead { get; private set; }

    [field: SerializeField]
    public ATurretShootController shootController { get; private set; }


    public void TryRotateHead(Vector3 dirVector)
    {
        Quaternion targetRotation = Quaternion.LookRotation(dirVector);

        transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
    }

    public void TryShoot(Vector3 dirToShoot)
    {
        shootController.Shoot(turretHead.ShootingFrom, dirToShoot);
    }

    public void AmmoSwitch(AAmmo newAmmo)
    {
        shootController.ChangeAmmo(newAmmo);
    }
  
}

