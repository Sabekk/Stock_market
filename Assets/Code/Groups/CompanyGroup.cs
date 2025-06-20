using Database.Group;
using Gameplay.Values;
using UnityEngine;

namespace Gameplay.Group
{
    public class CompanyGroup : IIdEqualable
    {
        #region VARIABLES

        private GroupData data;
        private ModifiableValue groupStockPrize;

        #endregion

        #region PROPERTIES

        public int Id => data.Id;
        public ModifiableValue GroupStockPrize => groupStockPrize;

        #endregion

        #region CONSTRUCTORS

        public CompanyGroup(GroupData groupData, float prizeMultipler)
        {
            data = groupData;
            groupStockPrize = new ModifiableValue(data.StockPrize * prizeMultipler, ValueType.OVERALL);
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
