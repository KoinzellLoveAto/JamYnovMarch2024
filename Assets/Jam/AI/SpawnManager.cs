using RakaExtension.ListExtension;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;

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
    private int EnemyToSpawn = 40;
    private int numEnemyLeft = 0;

    public Action OnWaveSpawn;

    public ScoreSC scoreSC;

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
        scoreSC.dmgCount += 100;
        currentEnemiesInMap.Remove(enemy);
        enemy.OnDeathEnemy -= HandlerDeathEnemy;
        Destroy(enemy.gameObject);

        if (currentEnemiesInMap.Count <= 0)
        {
            OnWaveClear?.Invoke();

        }

    }

    public void HandleEndWave()
    {

    }

    public void SpawnEntiereWave()
    {
        //character.transform.SetLocalPositionAndRotation(PlayerSpawn.position,PlayerSpawn.rotation);
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

    private void Update()
    {
        scoreSC = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreSC>();
    }
}
