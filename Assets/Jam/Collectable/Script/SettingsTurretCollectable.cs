using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsTurretCollectable : MonoBehaviour, ICollectable
{
    [field: SerializeField]
    public DataTurretShooter dataTurretSettings { get;private set; }

    [field: SerializeField]
    public GameObject _gameObjetMesh { get; private set; }

    private bool CannotBeTriggerAnymore = false;


    public void Collect(Character Collecter)
    {
        if (!CannotBeTriggerAnymore)
        {

            CannotBeTriggerAnymore = true;
            Collecter.AskToChangeSettingsTurret(dataTurretSettings);
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
