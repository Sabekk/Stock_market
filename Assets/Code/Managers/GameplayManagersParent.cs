using UnityEngine;

namespace Gameplay.Managment
{
    public class GameplayManagersParent : ManagersParent
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        protected override void SetManagers()
        {
            managers.AddRange(GetComponentsInChildren<IGameplayManager>());
        }

        #endregion
    }
}
