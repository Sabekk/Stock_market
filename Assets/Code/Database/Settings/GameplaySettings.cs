using UnityEngine;

namespace Database.Settings
{
    [CreateAssetMenu(fileName = "GameplaySettings", menuName = "Database/Settings/GameplaySettings")]
    public class GameplaySettings : ScriptableObject
    {
        #region VARIABLES

        [SerializeField] private GameplaySettings_Companies companiesSettings = new();
        [SerializeField] private GameplaySettings_Timing timingSettings = new();

        #endregion

        #region PROPERTIES

        public GameplaySettings_Companies CompaniesSettings => companiesSettings;
        public GameplaySettings_Timing TimingSettings => timingSettings;

        #endregion

        #region METHODS

        #endregion
    }
}