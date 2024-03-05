using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace RakaEngine.Pooling
{
    public class RakaPool<T> where T : MonoBehaviour, IPoolable
    {

        protected int m_poolSize;
        protected GameObject m_prefab;
        protected List<T> m_objectsPool;

        public IReadOnlyList<T> Objects => m_objectsPool;

        /// <summary>
        /// Return the object number in the pool
        /// </summary>
        public int ObjectCount => Objects.Count;
        public bool IsReady { get; private set; } = false;

        public Action OnPoolLoad;
        public Action OnPoolCleared;


        /// <summary>
        /// Build a Pool of yours object with a initSize (number max can me increased if no object are avaible when Get() )
        /// </summary>
        /// <param name="a_objetToPool"></param>
        /// <param name="a_poolSize"></param>
        /// <exception cref="ArgumentException"></exception>
        public RakaPool(T a_objetToPool, int a_poolSize)
        {
            if (a_objetToPool is not IPoolable)
            {
                throw new ArgumentException("You cannot create RakaPool because the object doesn't implement IPoolable Interface");
            }
            m_prefab = a_objetToPool.gameObject;
            m_poolSize = a_poolSize;
            m_objectsPool = new List<T>();

            InstanciateObjects();
            IsReady = true;
            OnPoolLoad?.Invoke();
        }

        /// <summary>
        /// Instanciate all objects with the poolsize assigned
        /// </summary>
        protected virtual void InstanciateObjects()
        {
            for (int index = 0; index < m_poolSize; index++)
            {
                T go = GameObject.Instantiate(m_prefab.GetComponent<T>());
                go.gameObject.SetActive(false);
                m_objectsPool.Add(go);

            }
        }

        /// <summary>
        /// Set actif or not all the list
        /// </summary>
        /// <param name="a_value"></param>
        internal void SetActiveAll(bool a_value)
        {
            foreach (T genericObject in m_objectsPool)
            {
                genericObject.gameObject.SetActive(a_value);
            }
        }

        /// <summary>
        /// Return the first object avaible, if not avaible, return a new one and add it to the objectList;
        /// </summary>
        /// <returns></returns>
        internal T GetObject()
        {
            foreach (T poolableObject in m_objectsPool)
            {
                if (poolableObject.CanBePooled())
                {
                    T wantedObject = poolableObject;
                    wantedObject.OnPull();
                    return poolableObject;
                }
            }

            //if not object found in the previous list
            Debug.LogWarning("Pool size was too short, a new object was Instancied");
            T newObject = GameObject.Instantiate(m_prefab, Vector3.zero, Quaternion.identity).GetComponent<T>();
            newObject.OnPull();
            m_objectsPool.Add(newObject);
            return newObject;
        }

        /// <summary>
        /// clear object list and destroy all objects
        /// </summary>
        internal void ClearObjects()
        {
            foreach (T poolableObject in m_objectsPool)
            {
                GameObject.Destroy(poolableObject);
            }

            m_objectsPool.Clear();
            OnPoolCleared?.Invoke();
        }

        /// <summary>
        /// Detach a object from the pool : the item removed will not be get anymore
        /// </summary>
        /// <param name="a_object"></param>
        internal void DetachFromPool(T a_object)
        {
            if (m_objectsPool.Contains(a_object))
                m_objectsPool.Remove(a_object);
            else //Nothing found
            Debug.LogWarning($"No item found, cannot detach [{a_object}] from the pool");
        }
    }
}