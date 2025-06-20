using Database.Group;
using System;
using UnityEngine;

namespace Database.Company
{
    [CreateAssetMenu(fileName = "CompanyData", menuName = "Database/Companies/CompanyData")]
    public class CompanyData : ScriptableObject, IIdEqualable
    {
        #region VARIABLES

        [SerializeField] private int id = Guid.NewGuid().GetHashCode();
        [SerializeField] private string companyName;
        [SerializeField] private float mainStockPrize;
        [SerializeField] private float groupsStockPrizeMultipler;
        [SerializeField] private GroupData[] startedGroups;

        #endregion

        #region PROPERTIES

        public int Id => id;
        public string CompanyName => companyName;
        public float MainStockPrize => mainStockPrize;
        public float GroupsStockPrizeMultipler => groupsStockPrizeMultipler;
        public GroupData[] StartedGroups => startedGroups;


        #endregion

        #region METHODS

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        #endregion
    }
}