using RakaExtension.ListExtension;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpawnManager : MonoBehaviour
{
    private List<AEnemy> currentEnemyInMap;

    [field: SerializeField]
    private List<Spawner> Spawner;

    [field: SerializeField]
    private List<AEnemy> EnemiesPrefab;



    private int EnemyToSpawn = 10;
    private int numEnemyLeft = 0;

    public Action OnWaveSpawn;

    public Action OnWaveClear;
    public void SpawnRndEnemy(Vector3 position)
    {
        AEnemy enemySpawned = Instantiate(EnemiesPrefab.PickRandom(), position, Quaternion.identity);
        currentEnemyInMap.Add(enemySpawned);
        enemySpawned.OnDeathEnemy += HandlerDeathEnemy;
    }


    public void SpawnEnemy(AEnemy enemy, Transform transformToSpawn)
    {
        Instantiate(enemy, transformToSpawn);

    }

    public void HandlerDeathEnemy(AEnemy enemy)
    {
        numEnemyLeft--;
        currentEnemyInMap.Remove(enemy);
        enemy.OnDeathEnemy -= HandlerDeathEnemy;

        if (numEnemyLeft == 0)
            OnWaveClear?.Invoke();

    }

    public void SpawnEntiereWave()
    {
        for (int i = 0; i < EnemyToSpawn; i++)
        {
            Spawner.PickRandom().GetRndPosition();
            SpawnRndEnemy(Spawner.PickRandom().GetRndPosition());
        }
    }
}
