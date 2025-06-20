using System;
using UnityEngine;

namespace Database.Settings
{
    [Serializable]
    public class GameplaySettings_Timing
    {
        #region VARIABLES

        [SerializeField] private int roundsCount;

        #endregion

        #region PROPERTIES

        public int RoundsCount => roundsCount;

        #endregion

        #region METHODS

        #endregion
    }
}
