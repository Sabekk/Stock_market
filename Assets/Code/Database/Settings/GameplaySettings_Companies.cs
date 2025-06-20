using System;
using UnityEngine;

namespace Database.Settings
{
    [Serializable]
    public class GameplaySettings_Companies
    {
        #region VARIABLES

        [SerializeField] private int minCompaniesInGame;
        [SerializeField] private int maxCompaniesInGame;

        #endregion

        #region PROPERTIES

        public int MinCompaniesInGame => minCompaniesInGame;
        public int MaxCompaniesInGame => maxCompaniesInGame;
        public int RandomCompaniesCount => UnityEngine.Random.Range(MinCompaniesInGame, MaxCompaniesInGame);

        #endregion

        #region METHODS

        #endregion
    }
}
