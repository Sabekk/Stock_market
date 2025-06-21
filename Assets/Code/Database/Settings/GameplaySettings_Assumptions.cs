using System;
using UnityEngine;

namespace Database.Settings
{
    [Serializable]
    public class GameplaySettings_Assumptions
    {
        #region VARIABLES

        [SerializeField] private int playerMoney;
        [SerializeField] private int roundsCount;

        #endregion

        #region PROPERTIES

        public int PlayerMoney => playerMoney;
        public int RoundsCount => roundsCount;

        #endregion

        #region METHODS

        #endregion
    }
}
