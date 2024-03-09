using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenRotation : MonoBehaviour
{
    Transform InitialTransform;

    public Vector3 RotationSpeed;

    private void Awake()
    {
        InitialTransform = transform;   
    }
    private void Update()
    {
        transform.Rotate(RotationSpeed * Time.deltaTime);
    }
}
