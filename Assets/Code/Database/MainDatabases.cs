using Database.Company;
using Database.Group;
using UnityEngine;

namespace Database
{
    [CreateAssetMenu(menuName = "Database/MainDatabases", fileName = "MainDatabases")]
    public class MainDatabases : ScriptableSingleton<MainDatabases>
    {
        #region VARIABLES

        [SerializeField] private CompaniesDatabase companiesDatabase;
        [SerializeField] private GroupsDatabase groupsDatabase;

        #endregion

        #region PROPERTIES

        public new static MainDatabases Instance => GetInstance("Singletons/MainDatabases");

        public CompaniesDatabase CompaniesDatabase => companiesDatabase;
        public GroupsDatabase GroupsDatabase => groupsDatabase;

        #endregion

        #region METHODS

        #endregion
    }
}