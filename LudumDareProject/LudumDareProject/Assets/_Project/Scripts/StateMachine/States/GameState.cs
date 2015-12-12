using UnityEngine;
using System.Collections;

public class GameState : MachineState 
{

	public GameEnvironment environment;

	public override void AInitialize ()
	{
		environment.Initialize();
	}

	public override void AEnable ()
	{
		environment.EnableSpawn();
	}

	public override void AUpdate ()
	{
		environment.AUpdate();
	}

	public override void ADisable ()
	{
		environment.DisableSpawn();
	}
}
