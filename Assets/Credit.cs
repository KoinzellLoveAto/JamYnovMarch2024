using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{
    [SerializeField]
    MapAction mapAction;
    public void Awake()
    {
        mapAction = new MapAction();
        mapAction.Menu.Enable();
        mapAction.Menu.Restart.performed += HandleQuit;

    }


    public void HandleQuit(InputAction.CallbackContext a_ctx)
    {
        mapAction.Menu.Disable();

        SceneManager.LoadScene("Menu");
    }

  
}
