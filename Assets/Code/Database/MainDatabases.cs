using Database.Company;
using Database.Group;
using Database.Settings;
using ObjectPooling;
using UnityEngine;

namespace Database
{
    [CreateAssetMenu(menuName = "Database/MainDatabases", fileName = "MainDatabases")]
    public class MainDatabases : ScriptableSingleton<MainDatabases>
    {
        #region VARIABLES

        [SerializeField] private GameplaySettings gameplaySettings;
        [SerializeField] private CompaniesDatabase companiesDatabase;
        [SerializeField] private GroupsDatabase groupsDatabase;
        [SerializeField] private ObjectPoolDatabase objectPoolDatabase;

        #endregion

        #region PROPERTIES

        public new static MainDatabases Instance => GetInstance("Singletons/MainDatabases");

        public GameplaySettings GameplaySettings => gameplaySettings;
        public CompaniesDatabase CompaniesDatabase => companiesDatabase;
        public GroupsDatabase GroupsDatabase => groupsDatabase;
        public ObjectPoolDatabase ObjectPoolDatabase => objectPoolDatabase;

        #endregion

        #region METHODS

        #endregion
    }
}