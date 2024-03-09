using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AAmmo : MonoBehaviour
{
    [field: SerializeField]
    public Rigidbody _rb;

    [field: SerializeField]
    public DataAmmo dataAmmo { get; private set; }

    public void Shoot(Vector3 dir, float force)
    {
        GiveForceToDir(dir, force);
        StartCoroutine(LifeTimeRoutine());
    }
    public void GiveForceToDir(Vector3 dir, float force)
    {
        _rb.AddForce(dir * force,ForceMode.Impulse);
    }

    public void KillProjectile()
    {
        Destroy(gameObject);
    }

    public IEnumerator LifeTimeRoutine()
    {
        yield return new WaitForSeconds(dataAmmo.LifeTime);
        Destroy(gameObject);
        
    }
}
