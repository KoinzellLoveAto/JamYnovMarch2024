using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableTrigger : MonoBehaviour
{
    [field: SerializeField]
    public Character ownerCharacter;

    private void OnTriggerEnter(Collider other)
    {
        ICollectable collectable = other.GetComponent<ICollectable>();
        if (collectable != null)
        {
            collectable.Collect(ownerCharacter);

        }
    }
}
