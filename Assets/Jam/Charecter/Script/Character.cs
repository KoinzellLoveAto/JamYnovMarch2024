using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using RakaEngine.Controllers;
using RakaEngine.Controllers.Health;
using RakaEngine.Controllers.Stamina;

public class Character : MonoBehaviour
{
    [field: Header("Camera")]
    [field: SerializeField]
    public CameraPlayer camPlayer { get; private set; }



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


    private Vector3 currentDirToMouse;

    private void Awake()
    {
        healthController.InitStat(100);
        healthController.EventSystem_onDeath += HandleDeath;
    }

    public void HandleDeath(HealthController healthController)
    {
        healthController.EventSystem_onDeath -= HandleDeath;


    }

    private void Update()
    {
        TryRotateTurret(currentDirToMouse);
    }

    public void TryMove(float direction)
    {
        movementController.Move(direction);

    }

    public void TryRotate(float rotation)
    {
        movementController.Rotate(rotation);
    }

    public void TryRotateTurret(Vector3 directionToLook)
    {
        turretEquiped.TryRotateHead(directionToLook);
    }


    public void TryShoot()
    {
       turretEquiped.TryShoot(currentDirToMouse);
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


    public void SetCurrentDirMouse(Vector2 lookInput)
    {
        /*
        Vector3 mouseWorldPosition = camPlayer.GetMouseWorldPosition(lookInput);

        Vector3 posTargetY0 = mouseWorldPosition;
        posTargetY0.y = transform.position.y;

        
        position.y = mouseWorldPosition.y;
*/
        Vector3 position = transform.position;

        Vector3 joystickDir = new Vector3(lookInput.x, 0, lookInput.y).normalized;

        Vector3 dir = (joystickDir * 20 - position).normalized;

        currentDirToMouse = joystickDir;
    }

    public void AskToChangeAmmo(AAmmo newAmmo)
    {
        turretEquiped.AmmoSwitch(newAmmo);
    }

    public void AskToChangeSettingsTurret(DataTurretShooter newSettingsController)
    {
        turretEquiped.ChangeTurretSetting(newSettingsController);
    }

    public void SetForwardDir()
    {
        currentDirToMouse = transform.forward;
        currentDirToMouse.y = 0;
        currentDirToMouse = currentDirToMouse.normalized * 20;
    }
}
