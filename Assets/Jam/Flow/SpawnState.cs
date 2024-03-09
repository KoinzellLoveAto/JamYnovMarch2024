using RakaEngine.Statemachine.Gameflow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnState : AGameState
{
    public SpawnManager spawnManager;
    
    public override void OnEnter()
    {
        base.OnEnter();
        spawnManager.SpawnEntiereWave();
        RequestDelayedChangeState(m_nextState, 2);
    }
}
