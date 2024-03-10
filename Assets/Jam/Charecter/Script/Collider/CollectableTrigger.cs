using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableTrigger : MonoBehaviour,IDamageable
{
    [field: SerializeField]
    public Character ownerCharacter;

    public void OnTakeCollectable()
    {

    }
    public void Damage(float amount)
    {
       ownerCharacter.healthController.Dammage(amount);
    }

    private void OnTriggerEnter(Collider other)
    {
        ICollectable collectable = other.GetComponent<ICollectable>();
        if (collectable != null)
        {
            collectable.Collect(ownerCharacter);
            OnTakeCollectable();
        }
    }
}
