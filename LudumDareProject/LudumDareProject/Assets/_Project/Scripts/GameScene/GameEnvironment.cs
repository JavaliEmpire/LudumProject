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

	}

	public void AUpdate()
	{
		_player.AUpdate();

		_obstacleManager.AUpdate();
	}


}
