using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RakaExtension.CameraExtension
{
    public static class CameraExtension 
    {
        /// <summary>
        /// Usable to see if a renderer is are in the bound vision of the cam
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="renderer"></param>
        /// <returns></returns>
        public static bool VisibleFromCamera(this Camera camera, Renderer renderer)
        {
            Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(camera);
            return GeometryUtility.TestPlanesAABB(frustumPlanes, renderer.bounds);
        }
    }
}
