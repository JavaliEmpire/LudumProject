using UnityEngine;
using System.Collections;

public class GameEnvironment : MonoBehaviour 
{

	#region Private Data

	[SerializeField] private PlayerController _player;

	[SerializeField] private ObstacleManager _obstacleManager;

	#endregion


	public void Initialize()
	{
		_obstacleManager.Initialize();
    }

	public void EnableSpawn()
	{
		_obstacleManager.EnableSpawner();
	}

	public void AUpdate()
	{
		_player.AUpdate();

		_obstacleManager.AUpdate();
	}

	public void DisableSpawn()
	{
		_obstacleManager.DisableSpawner();
	}
}
