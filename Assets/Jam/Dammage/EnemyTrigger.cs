using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour,IDamageable
{
    public AEnemy owner;
    public AudioSource damageSound;

    public void Damage(float amount)
    {
        owner.healthController.Dammage(amount);
        damageSound.Play();
    }

    public void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        
    }

}
