using UnityEngine;
using System.Collections;

public class GameState : MachineState 
{

	public GameEnvironment environment;

	public override void AInitialize ()
	{
		environment.Initialize();
	}

	public override void AUpdate ()
	{
		environment.AUpdate();
	}

}
