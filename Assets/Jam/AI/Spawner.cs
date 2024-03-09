using RakaExtension.ListExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Spawner : MonoBehaviour
{
    private List<AEnemy> currentEnemyInMap;

    [field: SerializeField]
    private List<AEnemy> EnemiesPrefab;

    private int EnemyToSpawn = 10;

    public void SpawnRndEnemy(Transform transformToSpawn)
    {
        Instantiate(EnemiesPrefab.PickRandom(), transformToSpawn);
    }


    public void SpawnEnemy(AEnemy enemy, Transform transformToSpawn)
    {
        Instantiate(enemy, transformToSpawn);

    }
}
