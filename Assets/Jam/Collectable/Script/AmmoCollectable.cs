using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AmmoCollectable : MonoBehaviour, ICollectable
{
    [field: SerializeField]
    private AAmmo _ammoPrefab { get; set; }

    [field: SerializeField]
    private GameObject _gameObjetMesh { get; set; }

    private bool CannotBeTriggerAnymore = false;
    public void Collect(Character Collectionner)
    {
        if (!CannotBeTriggerAnymore)
        {

            CannotBeTriggerAnymore = true;
            Collectionner.AskToChangeAmmo(_ammoPrefab);
            _gameObjetMesh.SetActive(false);
            StartCoroutine(DelayedDestroy());
        }
    }

    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
