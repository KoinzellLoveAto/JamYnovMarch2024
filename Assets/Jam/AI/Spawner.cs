using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public BoxCollider boxCollider;

    public Vector3 GetRndPosition()
    {
        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider reference is missing!");
        }

        // Obtenir les dimensions du BoxCollider
        Vector3 size = boxCollider.size;
        Vector3 center = boxCollider.center;

        // Calculer les valeurs minimales et maximales pour chaque axe
        float minX = center.x - size.x / 2f;
        float maxX = center.x + size.x / 2f;
        float minY = center.y - size.y / 2f;
        float maxY = center.y + size.y / 2f;
        float minZ = center.z - size.z / 2f;
        float maxZ = center.z + size.z / 2f;

        // Obtenir un point aléatoire dans la boîte
        Vector3 randomPoint = new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            Random.Range(minZ, maxZ)
        );

        return transform.position + randomPoint;
    }


}
