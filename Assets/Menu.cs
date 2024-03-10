using RakaEngine.UI.Panel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public MapAction mapAction;
    public void Awake()
    {
        mapAction = new MapAction();
        mapAction.Menu.Enable();
        mapAction.Menu.Restart.performed += HandleStart;
        mapAction.Menu.Leave.performed += HandleCredit;

    }


    public void HandleStart(InputAction.CallbackContext a_ctx)
    {
        mapAction.Menu.Disable();
        mapAction.Menu.Restart.performed -= HandleStart;
        mapAction.Menu.Leave.performed -= HandleCredit;

        SceneManager.LoadScene("FinalScene");
    }

    public void HandleCredit(InputAction.CallbackContext a_ctx)
    {
        SceneManager.LoadScene("Credit");

    }
}
