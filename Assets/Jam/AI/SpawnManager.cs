using RakaExtension.ListExtension;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class SpawnManager : MonoBehaviour
{
    private List<AEnemy> currentEnemiesInMap = new();

    [field: SerializeField]
    private List<Spawner> Spawner;

    [field: SerializeField]
    private List<AEnemy> EnemiesPrefab;

    [field: SerializeField]
    private Character character;

    public Transform PlayerSpawn;

    public int currentWave=0;
    private int EnemyToSpawn = 10;
    private int numEnemyLeft = 0;

    public Action OnWaveSpawn;

    public UnityEvent OnWaveClear;
    public void SpawnRndEnemy(Vector3 position)
    {
        AEnemy enemySpawned = Instantiate(EnemiesPrefab.PickRandom(), position, Quaternion.identity);
        currentEnemiesInMap.Add(enemySpawned);
        enemySpawned.Init(character.transform);
        enemySpawned.OnDeathEnemy += HandlerDeathEnemy;
    }


    public void SpawnEnemy(AEnemy enemy, Transform transformToSpawn)
    {
        Instantiate(enemy, transformToSpawn);

    }

    public void HandlerDeathEnemy(AEnemy enemy)
    {
        numEnemyLeft--;
        currentEnemiesInMap.Remove(enemy);
        enemy.OnDeathEnemy -= HandlerDeathEnemy;
        Destroy(enemy.gameObject);

        if (numEnemyLeft <= 0)
        {
            OnWaveClear?.Invoke();

        }

    }

    public void HandleEndWave()
    {

    }

    public void SpawnEntiereWave()
    {
        
        for (int i = 0; i < EnemyToSpawn; i++)
        {
            SpawnRndEnemy(Spawner.PickRandom().GetRndPosition());
        }
        currentWave++;
    }

    public void GiveTargetToAI(Character target)
    {
        foreach(var enemy in currentEnemiesInMap)
        {
            enemy.SetTarget(target.transform);
        }
    }

    public void SpawnNextWave()
    {
        EnemyToSpawn = EnemyToSpawn + currentWave * 5;
        SpawnEntiereWave();
    }
}
