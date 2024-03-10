using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChiottesExplosives : AAmmo
{
    [Header("Timer Settings")]
    [Space(5)]
    public float exploTimer;
    public float minTimer;
    public float maxTimer;

    [Space(10)]

    public GameObject meshObj;
    private int scalevfx = 5;

    [Header("Explosion Settings")]
    [Space(5)]
    public float exploForce;
    public float minRad;
    public float maxRad;
    [Space(5)]
    public float exploDuration;
    public float exploRadius;
    [Space(5)]
    public GameObject explosionVFX;
    private bool freeze = false;
    Transform attach;
    private void Awake()
    {
        exploTimer = Random.Range(minTimer, maxTimer);
        exploRadius = Random.Range(minRad, maxRad);
    }

    public void Update()
    {
        if (!freeze)
            transform.forward = _rb.velocity.normalized;
        else
        {

        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        freeze = true;
  
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        StartCoroutine(TimerExplosion());
    }

    public IEnumerator TimerExplosion()
    {
        print("coroutine");
        yield return new WaitForSecondsRealtime(exploTimer);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().AddExplosionForce(exploForce, transform.position, exploRadius);
        print("Kaboom!");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, exploRadius);
        foreach (var hitCollider in hitColliders)
        {
            //Damage Enemies;
            IDamageable damamge = hitCollider.gameObject.GetComponent<IDamageable>() ;
            if (damamge != null && hitCollider.gameObject.GetComponent<CollectableTrigger>() == null)
            {
                damamge.Damage(dataAmmo.Dammage);
            }
        }

        meshObj.SetActive(false);
       // explosionVFX.transform.localScale *= exploForce / 2;
        explosionVFX.SetActive(true);
    }


    public void ResetValues()
    {
        exploTimer = Random.Range(minTimer, maxTimer);
        exploForce = Random.Range(minRad, maxRad);
    }
}
