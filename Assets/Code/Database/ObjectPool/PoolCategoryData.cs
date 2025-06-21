using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Database;

namespace ObjectPooling
{
    [Serializable]
    public class PoolCategoryData
    {
        #region VARIABLES

        [SerializeField] private string categoryName;
        [SerializeField] private List<PoolInstancesGroupData> groups;

        #endregion

        #region PROPERTIES

        public string CategoryName => categoryName;
        public List<PoolInstanceData> Instances
        {
            get
            {
                List<PoolInstanceData> instances = new();
                for (int i = 0; i < groups.Count; i++)
                    instances.AddRange(groups[i].Instances);

                return instances;
            }
        }
        public List<PoolInstancesGroupData> Groups => groups;


        #endregion

        #region CONSTRUCTORS

        public PoolCategoryData()
        {
        }

        public PoolCategoryData(string name)
        {
            categoryName = name;
        }

        #endregion

        #region METHODS

        public void AddInstance(PoolInstanceData instance)
        {
            //instances.Add(instance);

            PoolInstancesGroupData group = FindGroup(instance.Name);
            if (group == null)
            {
                group = new(GetGroupName(instance.Name));
                Groups.Add(group);
            }

            group.AddInstance(instance);
        }

        public void RemoveInstance(PoolInstanceData instance)
        {
            //instances.Remove(instance);

            PoolInstancesGroupData group = FindGroup(instance.Name);
            if (group == null)
                return;

            group.RemoveInstance(instance);
            if (group.Instances.Count <= 0)
                Groups.Remove(group);
        }

        public PoolInstanceData FindInstanceData(string instanceDataName)
        {
            PoolInstancesGroupData group = FindGroup(GetGroupName(instanceDataName));
            if (group != null)
                return group.FindInstanceData(instanceDataName);
            return null;
        }


        public PoolInstancesGroupData FindGroup(string instanceDataName)
        {
            string groupName = GetGroupName(instanceDataName);
            return Groups.Find(x => x.GroupName == groupName);
        }

        public string GetGroupName(string instanceDataName)
        {
            int lastIndexOfNumber = instanceDataName.LastIndexOf("_");
            if (lastIndexOfNumber < 0)
                return instanceDataName;
            else
            {
                string groupName = instanceDataName.Remove(lastIndexOfNumber, instanceDataName.Length - lastIndexOfNumber);
                return groupName;
            }
        }

        #endregion
    }
}
