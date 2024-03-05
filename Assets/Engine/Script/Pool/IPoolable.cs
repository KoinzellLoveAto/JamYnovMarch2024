using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RakaEngine.Pooling
{
    public interface IPoolable
    {
        /// <summary>
        /// the condition to pooled a object
        /// </summary>
        public bool CanBePooled();

        /// <summary>
        /// Called when a object is pulled
        /// </summary>
        public void OnPull();

    }
}
