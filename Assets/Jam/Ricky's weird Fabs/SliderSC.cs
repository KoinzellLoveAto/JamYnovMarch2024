using RakaEngine.Controllers.Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSC : MonoBehaviour
{
    public Slider slid;
    public HealthController healthController;
    float maxHP;
    // Start is called before the first frame update
    void Start()
    {
        //slid = this.gameObject.GetComponent<Slider>();
        maxHP = healthController.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        print(maxHP + "and " + healthController.currentHealth);
        print((float)healthController.currentHealth / maxHP);
        slid.value = (float)healthController.currentHealth / maxHP;
        //slid.value = 0.5f;
    }
}
