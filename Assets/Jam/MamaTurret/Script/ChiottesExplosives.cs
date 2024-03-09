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




    private void Awake()
    {
        exploTimer = Random.Range(minTimer, maxTimer);
        exploRadius = Random.Range(minRad, maxRad);
    }

    private void OnCollisionEnter(Collision collision)
    {
        transform.parent = collision.transform;
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
        }

        explosionVFX.transform.localScale *= exploForce;
        explosionVFX.SetActive(true);
    }


    public void ResetValues()
    {
        exploTimer = Random.Range(minTimer, maxTimer);
        exploForce = Random.Range(minRad, maxRad);
    }
}
