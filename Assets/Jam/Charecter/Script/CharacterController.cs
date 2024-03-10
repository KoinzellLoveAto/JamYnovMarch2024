using RakaEngine.Controllers.Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    private MapAction mapActionPlayer;

    public Character linkedCharacter;


    private void Awake()
    {
        linkedCharacter.healthController.EventSystem_onDeath += HandleDeathPlayer;
        mapActionPlayer = new MapAction();
        mapActionPlayer.Character.Enable();
    }


    public void HandleDeathPlayer(HealthController healthCOntroller)
    {
        mapActionPlayer.Character.Disable();

    }

    public void Start()
    {
        mapActionPlayer.Character.Boost.canceled += HandleCancelBoost;

    }

    public void OnDestroy()
    {
        mapActionPlayer.Character.Boost.canceled -= HandleCancelBoost;

    }

    public void HandleCancelBoost(InputAction.CallbackContext a_ctx)
    {
        linkedCharacter.CancelBoost();
    }


    public void Update()
    {
        if (linkedCharacter)
        {
            Vector2 directionInput = mapActionPlayer.Character.Move.ReadValue<Vector2>();
            linkedCharacter.TryMove(directionInput.y);

            Vector2 rotateInput = mapActionPlayer.Character.Rotate.ReadValue<Vector2>();
            linkedCharacter.TryRotate(rotateInput.x);

            Vector2 lookInput = mapActionPlayer.Character.LookInput.ReadValue<Vector2>();
            //linkedCharacter.SetCurrentDirMouse(lookInput);

            if (mapActionPlayer.Character.Boost.IsPressed())
                linkedCharacter.TryUseBoost();

            if (mapActionPlayer.Character.Shoot.IsPressed())
                linkedCharacter.TryShoot();

            if(Mathf.Approximately(mapActionPlayer.Character.LookInput.ReadValue<Vector2>().magnitude,0))
            {
                linkedCharacter.SetForwardDir();
            }
            else
            {
                linkedCharacter.SetCurrentDirMouse(lookInput);
            }

        }
        else
            print("No character linked");
    }

}
