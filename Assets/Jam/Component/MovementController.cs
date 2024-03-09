using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{


    
    [field:SerializeField]
    public Rigidbody _rb { get; private set; }

    [field: SerializeField]
    public  Character OwnerCharact { get; private set; }

    public DataMovement dataBaseMovement;
    public DataMovement dataBoostMovement;

    private DataMovement _currentDataUse;

    private bool _canMove = true;

    public void Awake()
    {
        _currentDataUse = dataBaseMovement;
    }

    public void Move(float direction)
    {
        _rb.AddForce(OwnerCharact.transform.forward * direction * _currentDataUse.ForceSpeedStrenght);
        ClampMaxVelocity();

    }


    public void Rotate(float rotationDirection)
    {
        OwnerCharact.transform.Rotate(Vector3.up * rotationDirection * _currentDataUse.rotationSpeed);
    }
    


    public void ClampMaxVelocity()
    {
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _currentDataUse.MaxVelocity);

    }

    public void ApplyBoost()
    {
        _currentDataUse = dataBoostMovement;
    }
    public void ApplyBaseMovement()
    {
        _currentDataUse = dataBaseMovement;
    }
}
