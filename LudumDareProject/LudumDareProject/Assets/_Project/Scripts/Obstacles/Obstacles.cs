using UnityEngine;
using System.Collections;
using System;
public class Obstacles : MonoBehaviour
{

    #region Enum

    public enum ObstacleType
    {
        OBSTACLE,
        ENEMY
    }

    #endregion

    #region Protected Data

    [SerializeField] protected int _difficultyLevel;
    [SerializeField] protected ObstacleType _obstacleType;
    [SerializeField] protected float _speed;

    protected float _moveForce;
    protected Rigidbody2D _RB;

    #endregion

    public ObstacleType obstacleType
    {
        get { return _obstacleType; }
    }

    public int difficultyLevel
    {
        get { return _difficultyLevel; }
    }

    public virtual void AUpdate()
    {

    }

    protected virtual void OutOfScreen()
    {
    }
}
