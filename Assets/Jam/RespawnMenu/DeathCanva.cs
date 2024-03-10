using MoreMountains.Tools;
using RakaEngine.UI.Panel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DeathCanva : MonoBehaviour
{
    [SerializeField]
    RakaPanel deathPanel;
    MapAction mapAction;
    public void ShowPanel()
    {
        mapAction =new MapAction();
        deathPanel.Show();
        mapAction.Menu.Enable();
        mapAction.Menu.Restart.performed += HandleRetry;
        mapAction.Menu.Leave.performed += HandleExit;

    }


    public void HandleRetry(InputAction.CallbackContext a_ctx)
    {
        mapAction.Menu.Disable();
        mapAction.Menu.Restart.performed -= HandleRetry;
        mapAction.Menu.Leave.performed -= HandleExit;

        SceneManager.LoadScene("FinalScene");
    }

    public void HandleExit(InputAction.CallbackContext a_ctx)
    {
        Application.Quit(); 
    }


}
