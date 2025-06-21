using UnityEngine;

namespace Database.Settings
{
    [CreateAssetMenu(fileName = "GameplaySettings", menuName = "Database/Settings/GameplaySettings")]
    public class GameplaySettings : ScriptableObject
    {
        #region VARIABLES

        [SerializeField] private GameplaySettings_Companies companiesSettings = new();
        [SerializeField] private GameplaySettings_Assumptions assumptionsSettings = new();

        #endregion

        #region PROPERTIES

        public GameplaySettings_Companies CompaniesSettings => companiesSettings;
        public GameplaySettings_Assumptions AssumptionsSettings => assumptionsSettings;

        #endregion

        #region METHODS

        #endregion
    }
}