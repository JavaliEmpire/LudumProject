using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameModel : MonoBehaviour
{
    #region Enum

    public enum DataType
    {
        CHARACTER_EXPERIENCE,
        CHARACTER_LEVEL,
        GOLD,
        GAME_LEVEL
    }

    #endregion

    #region Public Data

    public Dictionary<string, int> _dictData;

    #endregion


    #region Static Data

    private static GameObject _GO;
    private static GameModel _instance;

    public static GameModel instance
    {
        get
        {
            if (_instance == null)
            {
                _GO = new GameObject("Game Model");
                DontDestroyOnLoad(_GO);
                _instance = _GO.AddComponent<GameModel>();
            }
            return _instance;
        }
    } 

	#endregion

    void Start()
    {
        _dictData = new Dictionary<string, int>();

        for (int i = 0; i < Enum.GetNames(typeof(DataType)).Length; i++)
        {
            DataType __dataType = (DataType)Enum.GetValues(typeof(DataType)).GetValue(i);

            _dictData.Add(__dataType.ToString(), 0);
        }

        LoadData();
    }

    public void SaveData()
    {
        foreach (KeyValuePair<string, int> __object in _dictData)
        {
            PlayerPrefs.SetInt(__object.Key, __object.Value);
        }
    }

    private void LoadData()
    {
        foreach (KeyValuePair<string, int> __object in _dictData)
        {
            if (PlayerPrefs.HasKey(__object.Key))
            {
                _dictData[__object.Key] = PlayerPrefs.GetInt(__object.Key);
            }
        }
    }
}
