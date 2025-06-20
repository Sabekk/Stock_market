using Database.Group;
using UnityEngine;

namespace Gameplay.Group
{
    public class CompanyGroup : IIdEqualable
    {
        #region VARIABLES

        private GroupData data;

        #endregion

        #region PROPERTIES

        public int Id => data.Id;

        #endregion

        #region CONSTRUCTORS

        public CompanyGroup(GroupData groupData)
        {
            data = groupData;
        }

        #endregion

        #region METHODS

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        #endregion
    }
}
