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

	#region Private Data

	[SerializeField] private StateType _currentState;

	private Dictionary<StateType, MachineState> _machineStatesDict;

	#endregion

	void Awake()
	{
		_machineStatesDict = new Dictionary<StateType, MachineState>();

		MachineState[] __states = GetComponentsInChildren<MachineState>(true);

		for (int i = 0; i < __states.Length; i++)
		{
			_machineStatesDict.Add(__states[i].stateType, __states[i]);
		}
	}
	

}
