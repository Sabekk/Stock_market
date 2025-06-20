using Database.Company;
using Gameplay.Group;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Company
{
    public class Company : IAttachableEvents, IIdEqualable
    {
        #region VARIABLES

        private List<CompanyGroup> groups;
        private CompanyData data;

        #endregion

        #region PROPERTIES

        public int Id => data.Id;
        public string CompanyName => data.CompanyName;
        public List<CompanyGroup> Groups => groups;

        #endregion

        #region CONSTRUCTORS

        public Company(CompanyData companyData)
        {
            data = companyData;
        }

        #endregion

        #region METHODS

        public void AddGroup(CompanyGroup newGroup)
        {
            groups.Add(newGroup);
            //TODO 
            //Add modifiable values
            //Add combine groups - (value1+value2)/2
        }

        public void RemoveGroup(CompanyGroup groupToRemove)
        {
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

        #endregion
    }
}