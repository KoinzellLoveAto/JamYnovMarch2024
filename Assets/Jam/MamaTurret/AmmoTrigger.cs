using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoTrigger : MonoBehaviour
{
    public AAmmo Owner;
    private void OnTriggerEnter(Collider other)
    {
        IDamageable trigger = other.GetComponent<IDamageable>();    

        if(trigger!=null)
        {
            trigger.Damage(Owner.dataAmmo.Dammage);
            Destroy(Owner.gameObject);
        }
    }
}
