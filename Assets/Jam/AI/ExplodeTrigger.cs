using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExplodeTrigger : MonoBehaviour
{
    public Action<IDamageable> onDammageableHit;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if(damageable != null)
        {
            onDammageableHit?.Invoke(damageable);
        }
    }

}
