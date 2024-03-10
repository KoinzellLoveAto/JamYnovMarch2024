using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour,IDamageable
{
    public AEnemy owner;


    public void Damage(float amount)
    {
        owner.healthController.Dammage(amount);
    }

    public void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        
    }

}
