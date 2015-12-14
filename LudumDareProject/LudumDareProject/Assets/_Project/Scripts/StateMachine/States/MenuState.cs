using UnityEngine;
using System.Collections;

public class MenuState : MachineState 
{
	[SerializeField] private MenuCanvas _menuCanvas;

	public override void AInitialize ()
	{

	}

	public override void AEnable ()
	{
		_menuCanvas.ListenEvents();
	}

	public override void AUpdate ()
	{

	}
}
