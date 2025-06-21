using Database.Company;
using Gameplay.Group;
using Gameplay.Values;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Companies
{
    [Serializable]
    public class Company : IAttachableEvents, IIdEqualable
    {
        #region VARIABLES

        [SerializeField] private ModifiableValue stockPrize;
        [SerializeField] private List<CompanyGroup> groups;

        private int currentSharesCount;
        private CompanyData data;

        #endregion

        #region PROPERTIES

        public int Id => data.Id;
        public int CurrentSharesCount => currentSharesCount;
        public float GroupStockPrizeMultipler => data.GroupsStockPrizeMultipler;
        public string CompanyName => data.CompanyName;
        public List<CompanyGroup> Groups => groups;
        public ModifiableValue StockPrize => stockPrize;

        #endregion

        #region CONSTRUCTORS

        public Company(CompanyData companyData)
        {
            data = companyData;
            stockPrize = new ModifiableValue(data.MainStockPrize, ValueType.OVERALL);
            currentSharesCount = data.SharesCount;
            InitializeGroups();
        }

        #endregion

        #region METHODS

        public int GetPrizeOfShares(int sharesCount)
        {
            return Mathf.RoundToInt(sharesCount * StockPrize.CurrentValue);
        }

        public void ChangeShares(int delta)
        {
            currentSharesCount += delta;
        }

        public void AddGroup(CompanyGroup newGroup)
        {
            CompanyGroup group = Groups.Find(x => x.IdEquals(newGroup.Id));
            if (group != null)
                group.GroupStockPrize.AddToRawValue(newGroup.GroupStockPrize.CurrentValue);
            else
            {
                stockPrize.AddNewComponent(newGroup.GroupStockPrize);
                Groups.Add(newGroup);
            }
        }

        public void RemoveGroup(CompanyGroup groupToRemove)
        {
            stockPrize.RemoveComponent(groupToRemove.GroupStockPrize);
            Groups.Remove(groupToRemove);
        }

        public void AttachEvents()
        {

        }

        public void DetachEvents()
        {

        }

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        private void InitializeGroups()
        {
            groups = new List<CompanyGroup>();

            foreach (var startingGroupData in data.StartedGroups)
            {
                CompanyGroup group = new CompanyGroup(startingGroupData, GroupStockPrizeMultipler);
                AddGroup(group);
            }
        }

        #endregion
    }
}