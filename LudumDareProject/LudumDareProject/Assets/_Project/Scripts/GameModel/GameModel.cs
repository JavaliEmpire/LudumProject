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
		GOLD_MULT,
    }

    #endregion

    #region Public Data

    public Dictionary<string, int> dictData;

	public Action refreshStatsAction;

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

    void Awake()
    {
        dictData = new Dictionary<string, int>();

        for (int i = 0; i < Enum.GetNames(typeof(DataType)).Length; i++)
        {
            DataType __dataType = (DataType)Enum.GetValues(typeof(DataType)).GetValue(i);

            dictData.Add(__dataType.ToString(), 1);
        }

		Debug.Log(dictData.Keys);
        LoadData();
    }

	public void AddExp(int p_exp)
	{
		dictData[DataType.CHARACTER_EXPERIENCE.ToString()] += p_exp;

		int __characterLevel = dictData[GameModel.DataType.CHARACTER_LEVEL.ToString()];

		int __expToNextLevel = Mathf.CeilToInt(Mathf.Pow((float)__characterLevel, 2f) * 10);

		if (dictData[DataType.CHARACTER_EXPERIENCE.ToString()] >= __expToNextLevel)
		{
			dictData[DataType.CHARACTER_LEVEL.ToString()] += 1;
			dictData[DataType.CHARACTER_EXPERIENCE.ToString()] = 0;

			refreshStatsAction();
		}
	}

    public void SaveData()
    {
        foreach (KeyValuePair<string, int> __object in dictData)
        {
            PlayerPrefs.SetInt(__object.Key, __object.Value);
        }
    }

    private void LoadData()
    {
        foreach (KeyValuePair<string, int> __object in dictData)
        {
			Debug.Log(__object.Key);

            if (PlayerPrefs.HasKey(__object.Key))
            {
				Debug.Log("has key");

				int __value = PlayerPrefs.GetInt(__object.Key);

				Debug.Log("Derp");

				dictData[__object.Key] = __value;
            }
        }
    }
}
