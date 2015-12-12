using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ObstacleManager : MonoBehaviour
{
    #region Private Data

    private Dictionary<int, List<GameObject>> _dictObstacles;

    private List<GameObject> listObstaclesInstances;

    private int _currentLevel = 1;

    [SerializeField] private float _spawnRate = 10f;

    private Vector3 _spawnPosition = new Vector3(15, -1, 0);

    #endregion

    void Awake()
    {
        LoadData();

        listObstaclesInstances = new List<GameObject>();
		
        SpawnRandomObstacle();
    }

    public void AUpdate()
    {
        for (int i = 0; i < listObstaclesInstances.Count; i ++)
        {
            if (listObstaclesInstances[i] == null)
            {
                listObstaclesInstances.RemoveAt(i);
                i--;
                continue;
            }           
            listObstaclesInstances[i].GetComponent<Obstacles>().AUpdate();
        }
    }

    private void SpawnRandomObstacle()
    {
        int __randomDifficultyLevel = UnityEngine.Random.Range(1, _currentLevel);
        int __randomObstacleIndex = UnityEngine.Random.Range(1, _dictObstacles[__randomDifficultyLevel].Count);       
        GameObject __newObstacle = _dictObstacles[__randomDifficultyLevel][__randomObstacleIndex];
        switch (__newObstacle.GetComponent<Obstacles>().obstacleType)
        {
            case Obstacles.ObstacleType.ENEMY:
                _spawnPosition.y = 2f;
                break;
            case Obstacles.ObstacleType.OBSTACLE:
                _spawnPosition.y = UnityEngine.Random.Range(-1f, 1f);
                break;
        }  
        __newObstacle = Instantiate(__newObstacle, _spawnPosition, Quaternion.identity) as GameObject;
        __newObstacle.transform.parent = this.transform;
        listObstaclesInstances.Add(__newObstacle);
        ATimer.WaitSeconds(_spawnRate, delegate
        {
            SpawnRandomObstacle();
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