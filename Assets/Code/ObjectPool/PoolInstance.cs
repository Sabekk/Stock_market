using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    [Serializable]
    public class PoolInstance
    {
        #region VARIABLES

        private string name;
        private string category;
        private GameObject prefab;
        private Transform instanceParent;
        private Stack<PoolObject> objects;
        private List<PoolObject> taken;

        #endregion

        #region PROPERTIES
        public string Name => name;

        #endregion

        #region CONSTRUCTORS

        public PoolInstance(PoolInstanceData data, string category, Transform parent)
        {
            name = data.Name;
            prefab = data.PoolObject;
            objects = new Stack<PoolObject>(data.Size);
            taken = new List<PoolObject>(data.Size);
            this.category = category;

            instanceParent = new GameObject(name).transform;
            instanceParent.SetParent(parent);

            for (int i = 0; i < data.Size; i++)
                AddToPool();
        }

        #endregion

        #region METHODS

        public PoolObject GetFromPool(bool activateObject = true)
        {
            if (objects.Count == 0)
                AddToPool();
            PoolObject poolObject = objects.Pop();
            poolObject.SetTaken(true);
            poolObject.Prefab.SetActive(activateObject);
            taken.Add(poolObject);
            return poolObject;
        }

        public void ReturnToPool(PoolObject poolObject)
        {
            objects.Push(poolObject);
            poolObject.SetTaken(false);
            poolObject.Prefab.SetActive(false);
            poolObject.Prefab.transform.SetParent(instanceParent);

            poolObject.Prefab.transform.position = Vector3.zero;
            poolObject.Prefab.transform.rotation = Quaternion.identity;
            poolObject.Prefab.transform.localScale = Vector3.one;

            taken.Remove(poolObject);
        }

        private void AddToPool()
        {
            if (prefab == null)
                return;

            GameObject newPoolObject = GameObject.Instantiate(prefab);
            newPoolObject.name = newPoolObject.name + "_Pool";
            ReturnToPool(new PoolObject(name, category, newPoolObject));
        }

        #endregion
    }
}