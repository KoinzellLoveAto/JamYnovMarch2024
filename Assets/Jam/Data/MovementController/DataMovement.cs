using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Component/MovementController")]
public class DataMovement : ScriptableObject
{
    [Header("Speed forward backward")]
    [Tooltip("Utilise le pour determiner la speed avec l'input du joueur")]
    public float ForceSpeedStrenght =2;

    [Tooltip("Max speed joueur")]
    public float MaxVelocity=10;


    [Header("RotationSpeed")]
    public float rotationSpeed=0.8f;



}
