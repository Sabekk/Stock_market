using UnityEngine;

namespace Database.Group
{
    [CreateAssetMenu(fileName = "GroupsDatabase", menuName = "Database/Groups/GroupsDatabase")]
    public class GroupsDatabase : ScriptableObject
    {
        #region VARIABLES

        [SerializeField] private GroupData[] groupsDatas;

        #endregion

        #region PROPERTIES

        public GroupData[] GroupsDatas => groupsDatas;

        #endregion

        #region METHODS

        #endregion
    }
}