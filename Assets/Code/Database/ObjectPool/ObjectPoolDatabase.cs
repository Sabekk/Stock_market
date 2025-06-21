using Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    [CreateAssetMenu(fileName = "ObjectPoolDatabase", menuName = "Database/ObjectPool")]
    public class ObjectPoolDatabase : ScriptableObject
    {
        #region VARIABLES

        [SerializeField] private List<PoolCategoryData> poolCategories;

        #endregion

        #region PROPERTIES

        public List<PoolCategoryData> PoolCategories => poolCategories;

        #endregion

        #region METHODS

        public void AddCategory(string name)
        {
            poolCategories.Add(new PoolCategoryData(name));
        }

        public void RemoveCategory(PoolCategoryData categoryToDelete)
        {
            poolCategories.Remove(categoryToDelete);
        }

        public PoolCategoryData FindCategoryData(string categoryDataName)
        {
            foreach (var poolCategory in PoolCategories)
            {
                if (poolCategory.CategoryName == categoryDataName)
                    return poolCategory;
            }
            return null;
        }

        #endregion
    }
}
