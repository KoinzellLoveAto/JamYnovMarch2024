using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSC : MonoBehaviour
{
    public int score;
    public int wave;
    public int dmgCount;
    public int enemyCount;

    public TextMeshProUGUI textScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score = dmgCount + (wave * enemyCount);

        textScore.text = score.ToString();
    }
}
