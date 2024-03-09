using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPlayer : MonoBehaviour
{
    [field: SerializeField]
    public CinemachineVirtualCamera VCam { get; private set; }

    public GameObject prefab;

    int layerMask;

    public void Awake()
    {
        layerMask = 1 << LayerMask.NameToLayer("Enemy");
    }
    public Vector3 GetMouseWorldPosition(Vector2 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        Vector3 intersectionPoint = Vector3.zero;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {

                intersectionPoint = hit.point;
        }
        else
        {
            intersectionPoint = ray.GetPoint(100);
        }

        return intersectionPoint;
    }

    public void shoot()
    {
    }
}
