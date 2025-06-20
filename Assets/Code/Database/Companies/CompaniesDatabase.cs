using UnityEngine;

namespace Database.Company
{
    [CreateAssetMenu(fileName = "CompaniesDatabase", menuName = "Database/Companies/CompaniesDatabase")]
    public class CompaniesDatabase : ScriptableObject
    {
        #region VARIABLES

        [SerializeField] private CompanyData[] companyDatas;

        #endregion

        #region PROPERTIES

        public CompanyData[] CompanyDatas => companyDatas;

        #endregion

        #region METHODS

        #endregion
    }
}