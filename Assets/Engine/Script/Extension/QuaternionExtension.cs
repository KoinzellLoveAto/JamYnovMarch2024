using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace RakaExtension.QuaternionExtension
{
    public static class QuaternionExtension
    {
        public static Quaternion GetRndRotation()
        {
            Quaternion rotation = new Quaternion();
            rotation.x = Random.Range(0f, 360f);
            rotation.y = Random.Range(0f, 360f);
            rotation.z = Random.Range(0f, 360f);

            return rotation;
        }
    }
}
