using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachine : MonoBehaviour 
{
	#region Enum

	public enum StateType
	{
		MENU,
		GAME,
	}

	#endregion

	#region Static Data

	private static StateMachine _instance;
	public static StateMachine instance
	{
		get { return _instance; }
	}

	#endregion

	#region Private Data

	[SerializeField] private StateType _currentState;

	private Dictionary<StateType, MachineState> _machineStatesDict;

	#endregion

	void Awake()
	{
		_instance = gameObject.GetComponent<StateMachine>();
	}

	void Start()
	{
		_machineStatesDict = new Dictionary<StateType, MachineState>();
		
		MachineState[] __states = GetComponentsInChildren<MachineState>(true);
		
		for (int i = 0; i < __states.Length; i++)
		{
			_machineStatesDict.Add(__states[i].stateType, __states[i]);
			__states[i].AInitialize();
		}

		ChangeState(_currentState);


	}

	void Update()
	{
		MachineState __currentState = null;
		_machineStatesDict.TryGetValue(_currentState, out __currentState);

		__currentState.AUpdate();
	}

	public static void ChangeState(StateType p_sateType)
	{
		StateMachine.instance.ChangeCurrentState(p_sateType);
	}

	private void ChangeCurrentState(StateType p_stateType)
	{
		foreach (KeyValuePair<StateType, MachineState> __state in _machineStatesDict)
		{
			if (__state.Value.stateType != p_stateType)
			{
				__state.Value.gameObject.SetActive(false);
			}
			else
			{
				__state.Value.gameObject.SetActive(true);
			}
		}

		_currentState = p_stateType;
	}
}
