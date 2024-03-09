using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPerception : MonoBehaviour
{
    public AEnemy owner;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Character>())
        {
            owner.OnPlayerInRange();
        }
    }
}
