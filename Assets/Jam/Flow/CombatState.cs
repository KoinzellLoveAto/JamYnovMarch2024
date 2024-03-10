using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RakaEngine.GameFlow;
using RakaEngine.Statemachine.Gameflow;
using JetBrains.Annotations;
using RakaEngine.Controllers.Health;

public class CombatState : AGameState
{
    public  SpawnManager spawnManager;
    public SpawnState spawnState;
    public Character player;
    public override void OnEnter()
    {
        base.OnEnter();
        spawnManager.GiveTargetToAI(player);
        player.healthController.EventSystem_onDeath += HandlerDeathPlayer;
    }

    public void HandlerDeathPlayer(HealthController hc)
    {
        player.healthController.EventSystem_onDeath -= HandlerDeathPlayer;
        RequestChangeState(m_nextState);
    }
    public void HandleWaveWiped()
    {
        player.healthController.EventSystem_onDeath -= HandlerDeathPlayer;
        RequestDelayedChangeState(spawnState,1);
    }
}
