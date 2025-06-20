using Database.Company;
using Gameplay.Group;
using Gameplay.Values;
using System.Collections.Generic;

namespace Gameplay.Company
{
    public class Company : IAttachableEvents, IIdEqualable
    {
        #region VARIABLES

        private List<CompanyGroup> groups;
        private CompanyData data;

        private ModifiableValue stockPrize;

        #endregion

        #region PROPERTIES

        public int Id => data.Id;
        public float GroupStockPrizeMultipler => data.GroupsStockPrizeMultipler;
        public string CompanyName => data.CompanyName;
        public List<CompanyGroup> Groups => groups;

        #endregion

        #region CONSTRUCTORS

        public Company(CompanyData companyData)
        {
            data = companyData;
            stockPrize = new ModifiableValue(data.MainStockPrize, ValueType.OVERALL);
            InitializeGroups();
        }

        #endregion

        #region METHODS

        public void AddGroup(CompanyGroup newGroup)
        {
            CompanyGroup group = groups.Find(x => x.IdEquals(newGroup.Id));
            if (group != null)
                group.GroupStockPrize.AddToRawValue(newGroup.GroupStockPrize.CurrentValue);
            else
            {
                stockPrize.AddNewComponent(newGroup.GroupStockPrize);
                groups.Add(newGroup);
            }
        }

        public void RemoveGroup(CompanyGroup groupToRemove)
        {
            stockPrize.RemoveComponent(groupToRemove.GroupStockPrize);
            groups.Remove(groupToRemove);
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
            foreach (var startingGroupData in data.StartedGroups)
            {
                CompanyGroup group = new CompanyGroup(startingGroupData, GroupStockPrizeMultipler);
                AddGroup(group);
            }
        }

        #endregion
    }
}