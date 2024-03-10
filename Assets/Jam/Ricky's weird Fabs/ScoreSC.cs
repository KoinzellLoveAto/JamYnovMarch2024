using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSC : MonoBehaviour
{
    public int score;
    public int wave;
    public int dmgCount;

    public SpawnManager spawnManager;


    public TextMeshProUGUI textScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        wave = spawnManager.currentWave;

        score = dmgCount * wave;

        textScore.text = score.ToString();
    }
}
