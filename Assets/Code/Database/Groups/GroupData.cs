using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GroupData", menuName = "Groups/GroupData")]
public class GroupData : ScriptableObject, IIdEqualable
{
    #region VARIABLES

    [SerializeField] private int id = Guid.NewGuid().GetHashCode();
    [SerializeField] private string groupName;
    [SerializeField] private float stockPrize;

    #endregion

    #region PROPERTIES

    public int Id => id;
    public string GroupName => groupName;
    public float StockPrize => stockPrize;


    #endregion

    #region METHODS

    public bool IdEquals(int id)
    {
        return Id == id;
    }

    #endregion
}
