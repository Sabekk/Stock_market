using System.Collections.Generic;
using UnityEngine;
using Gameplay.Managment;
using Database;
using Database.Company;

namespace Gameplay.Companies
{
    public class CompaniesManager : GameplayManager<CompaniesManager>
    {
        #region VARIABLES

        [SerializeField] private List<Company> companies;

        #endregion

        #region PROPERTIES

        public List<Company> Companies => companies;
        private MainDatabases Database => MainDatabases.Instance;

        #endregion

        #region UNITY_METHODS

        protected override void Awake()
        {
            base.Awake();
            companies = new List<Company>();
        }

        #endregion

        #region METHODS

        public override void Initialzie()
        {
            base.Initialzie();
            InitializeCompanies();
        }

        public Company TryGetCompanyById(int companyId)
        {
            Company company = CompaniesManager.Instance.Companies.GetElementById(companyId);
            if (company == null)
            {
                Debug.LogError($"Company id {companyId}, didn't found");
                return null;
            }

            return company;
        }

        private void InitializeCompanies()
        {
            int randomCount = Database.GameplaySettings.CompaniesSettings.RandomCompaniesCount;
            List<CompanyData> companyDatas = new List<CompanyData>();
            companyDatas.AddRange(Database.CompaniesDatabase.CompanyDatas);

            if (randomCount > companyDatas.Count - 1)
                randomCount = companyDatas.Count - 1;

            CompanyData currentData = null;

            for (int i = 0; i < randomCount; i++)
            {
                currentData = companyDatas[Random.Range(0, companyDatas.Count)];
                companyDatas.Remove(currentData);
                companies.Add(new Company(currentData));

                currentData = null;
            }
        }

        #endregion
    }
}