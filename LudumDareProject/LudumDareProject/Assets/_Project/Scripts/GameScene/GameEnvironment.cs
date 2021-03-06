﻿using UnityEngine;
using System.Collections;
using System;

public class GameEnvironment : MonoBehaviour 
{

	#region Enum 
	
	public enum GameStateType
	{
		PLAYING,
		ON_HOLD,
		STORE
	}
	
	#endregion

	#region Private Data
	
	private GameStateType _currentGameState;

	[SerializeField] private PlayerController _player;

	[SerializeField] private ObstacleManager _obstacleManager;

	[SerializeField] private GameCanvas _gameCanvas;

    [SerializeField] private Background _background;

	#endregion

	public void Initialize()
	{
		_gameCanvas.onGoButtonPress += delegate 
		{
			ChangeState(GameStateType.PLAYING, false);
			EnableSpawn();
		};

		_gameCanvas.onBackButtonPress += delegate 
		{
			StateMachine.ChangeState(StateMachine.StateType.MENU);
		};

		_gameCanvas.Initialize();

		_player.onOutOfScreen += delegate (Action p_action)
		{
			ChangeState(GameStateType.ON_HOLD, false);

			DisableSpawn();

			GameModel.instance.dictData[GameModel.DataType.CHARACTER_EXPERIENCE.ToString()] = 0;

			GameModel.instance.refreshStatsAction();

			FadeCanvas.instance.FadeTo(p_action, delegate 
			{
				_gameCanvas.ShowOnHoldGroup(_gameCanvas.ListenButtonEvents);
			});
		};

		_obstacleManager.Initialize();
		_currentGameState = GameStateType.ON_HOLD;

		GameModel.instance.modifyParallaxMultiplierAction += delegate (float p_value)
		{
			_background.SetMultiplier(p_value);
		};		                                                        
    }

	private void EnableSpawn()
	{
		_obstacleManager.EnableSpawner();
	}

	private void DisableSpawn()
	{
		_obstacleManager.DisableSpawner();
	}

	public void AUpdate()
	{
		if (_currentGameState == GameStateType.PLAYING)
		{
			_player.AUpdate();

			_obstacleManager.AUpdate();
		}
	}

	public void ChangeState(GameStateType p_state, bool p_applyTransition)
	{
		_currentGameState = p_state;

		if (_currentGameState == GameStateType.ON_HOLD)
		{
			GameModel.instance.refreshStatsAction();
			GameModel.instance.SaveData();
		}

		if (p_applyTransition == false)
			return;

		switch 	(_currentGameState)
		{
		case GameStateType.ON_HOLD:
			_gameCanvas.ShowOnHoldGroup(_gameCanvas.ListenButtonEvents);

			break;

		}
	}


}
