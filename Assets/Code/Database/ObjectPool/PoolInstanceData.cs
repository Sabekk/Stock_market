using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ObjectPooling
{
    [Serializable]
    public class PoolInstanceData
    {
        #region VARIABLES

        [SerializeField] private string name;
        [SerializeField] private GameObject poolObject;
        [SerializeField] private int size;

        #endregion

        #region PROPERTIES

        public string Name => name;
        public GameObject PoolObject => poolObject;
        public int Size => size;

        #endregion

        #region CONSTRUCTOES

        public PoolInstanceData() { }
        public PoolInstanceData(string name, GameObject poolObject, int size)
        {
            this.name = name;
            this.poolObject = poolObject;
            this.size = size;
        }

        #endregion

        #region METHODS

        #endregion
    }
}
