using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoapAmmo : AAmmo
{

    public float force;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Rigidbody>() != null)
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(GetComponent<Rigidbody>().velocity.normalized * force);
        }
    }

}
