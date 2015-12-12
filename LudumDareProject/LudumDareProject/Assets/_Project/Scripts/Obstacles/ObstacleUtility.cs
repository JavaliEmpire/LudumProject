using UnityEngine;
using System.Collections;

public class ObstacleUtility : Obstacles
{

    #region Enumerators
    public enum UtilitiesType
    {
        JUMP,
    }
    #endregion

    [SerializeField] protected UtilitiesType _utilitieType;

    protected virtual void OnTriggerEnter2D(Collider2D p_other)
    {

    }
}
