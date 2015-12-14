using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ObstacleManager : MonoBehaviour
{
    #region Private Data

    private Dictionary<int, List<GameObject>> _dictObstacles;

	private List<GameObject> _listObstaclesInstances = new List<GameObject>();

    private int _currentLevel = 1;

    [SerializeField] private float _spawnRate = 10f;

    private Vector3 _spawnPosition = new Vector3(15, -1, 0);
	
	ATimerNodule _spawnTimer;

    #endregion

    public void Initialize()
    {
		LoadData();
    }

	public void EnableSpawner()
	{
		SpawnNewObstacle();
	}

	public void DisableSpawner()
	{
		try { _spawnTimer.Stop(); }
		catch{}

		for (int i = 0; i < _listObstaclesInstances.Count; i++)
		{
			Destroy(_listObstaclesInstances[i]);
		}

		_listObstaclesInstances.Clear();
	}

    public void AUpdate()
    {
        for (int i = 0; i < _listObstaclesInstances.Count; i ++)
        {
            if (_listObstaclesInstances[i] == null)
            {
                _listObstaclesInstances.RemoveAt(i);
                i--;
                continue;
            }           
            _listObstaclesInstances[i].GetComponent<Obstacles>().AUpdate();
        }
    }

    private void SpawnNewObstacle()
    {
        int __randomDifficultyLevel = UnityEngine.Random.Range(1, _currentLevel);
        int __randomObstacleIndex = UnityEngine.Random.Range(0, _dictObstacles[__randomDifficultyLevel].Count);

        string __obstacleName;

        GameObject __newObstacle = _dictObstacles[__randomDifficultyLevel][__randomObstacleIndex];
        __obstacleName = __newObstacle.name;
        switch (__newObstacle.GetComponent<Obstacles>().obstacleType)
        {
            case Obstacles.ObstacleType.ENEMY:
                _spawnPosition.y = 8f;    
                break;
            case Obstacles.ObstacleType.OBSTACLE:
                _spawnPosition.y = 0.9f;
                break;
        }
        _spawnPosition.z = 0f;
        __newObstacle = Instantiate(__newObstacle, _spawnPosition, Quaternion.identity) as GameObject;
        __newObstacle.name = __obstacleName;
        __newObstacle.transform.parent = this.transform;

        _listObstaclesInstances.Add(__newObstacle);

        _spawnTimer = ATimer.WaitSeconds(_spawnRate, delegate
        {
            if (__newObstacle.name == "Obstacle Jump 1")
            {
                SpawnObstacle("Obstacle Bridge 2");
            }
            else
            {
                SpawnNewObstacle();
            }
        });
    }

    private void SpawnObstacle (string p_obstacleName)
    {
        int __targetObstacleIndex = 0;
        for (int i = 0; i < _dictObstacles[_currentLevel].Count; i ++)
        {
            if (_dictObstacles[_currentLevel][i].name == p_obstacleName)
            {
                __targetObstacleIndex = i;
                break;
            }
        }

        GameObject __newObstacle = _dictObstacles[_currentLevel][__targetObstacleIndex];
        switch (__newObstacle.GetComponent<Obstacles>().obstacleType)
        {
            case Obstacles.ObstacleType.ENEMY:
                _spawnPosition.y = 2f;
                break;
            case Obstacles.ObstacleType.OBSTACLE:
                _spawnPosition.y = 0.9f;
                break;
        }
        _spawnPosition.z = 0f;

        __newObstacle = Instantiate(__newObstacle, _spawnPosition, Quaternion.identity) as GameObject;

        __newObstacle.transform.parent = this.transform;

        _listObstaclesInstances.Add(__newObstacle);

        _spawnTimer = ATimer.WaitSeconds(_spawnRate, delegate
        {
            SpawnNewObstacle();
        });
    }

    private void LoadData()
    {
        _dictObstacles = new Dictionary<int, List<GameObject>>();

        GameObject[] __obstaclePrefabs = Resources.LoadAll<GameObject>("Obstacles");

        int __levelMax = 0;

        for (int i = 0; i < __obstaclePrefabs.Length; i ++)
        {
            int __currentDifficultyLevel = __obstaclePrefabs[i].GetComponent<Obstacles>().difficultyLevel;

            if (__levelMax < __currentDifficultyLevel)
            {
                __levelMax = __currentDifficultyLevel;
            }
        }

        for (int i = 1; i <= __levelMax; i ++)
        {
            List<GameObject> __listObstaclesOfLevel = new List<GameObject>();

            for (int j = 0; j < __obstaclePrefabs.Length; j++)
            {
                if (i == __obstaclePrefabs[j].GetComponent<Obstacles>().difficultyLevel)
                {
                    __listObstaclesOfLevel.Add(__obstaclePrefabs[j]);
                }
            }
            _dictObstacles.Add(i, __listObstaclesOfLevel);
        }

    }
}