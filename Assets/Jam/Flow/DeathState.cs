using RakaEngine.Statemachine.Gameflow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : AGameState
{
    public DeathCanva deathCanva;


    public override void OnEnter()
    {
        base.OnEnter();
        deathCanva.ShowPanel();
    }
}
