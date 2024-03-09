using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using RakaEngine.Controllers;
using RakaEngine.Controllers.Health;
using RakaEngine.Controllers.Stamina;

public class Character : MonoBehaviour
{
    [field:SerializeField]
    public CinemachineVirtualCamera VCam { get; private set; }


    [field:Header("Components")]
    [field: SerializeField]
    public MovementController movementController { get; private set; }

    [field: SerializeField]
    public HealthController healthController { get; private set; }
    [field: SerializeField]
    public BoostController boostController { get; private set; }


    [field:Header("Turret")]
    [field: SerializeField]
    public Turret turretEquiped { get; private set; }


    public void TryMove(float direction)
    {
        movementController.Move(direction);

    }

    public void TryRotate(float rotation)
    {
        movementController.Rotate(rotation);
    }

    public void TryShoot()
    {
        print("shoot");
    }


    public void TryUseBoost()
    {
            movementController.ApplyBoost();
        if (boostController.HaveEnoughtBoost(1))
        {
            boostController.ConsumeBoost(0.1f);

        }
    }

    public void CancelBoost()
    {
        movementController.ApplyBaseMovement();
    }

}
