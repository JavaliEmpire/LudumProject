using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuCanvas : MonoBehaviour 
{
	#region Public Data

	public ButtonEffects playButton;

	public ButtonEffects creditsButton;

	public ButtonEffects exitButton;

	#endregion

	public void ListenEvents()
	{
		DisableInputs();

		playButton.onButtonClick += HandlePlayButtonPres;
		exitButton.onButtonClick += HandleExitButtonPress;
		creditsButton.onButtonClick += HandleCreditsButtonPress;
	}

	void HandlePlayButtonPres()
	{
		DisableInputs();

		FadeCanvas.instance.FadeTo(delegate 
		{
			StateMachine.ChangeState(StateMachine.StateType.GAME);
		}, null);
	}

	void HandleExitButtonPress()
	{
		DisableInputs();

		FadeCanvas.instance.FadeTo(delegate 
		{
			Application.Quit();
		}, null);
	}

	void HandleCreditsButtonPress()
	{
		DisableInputs();

		FadeCanvas.instance.FadeTo(delegate 
		{
			StateMachine.ChangeState(StateMachine.StateType.CREDITS);
		}, null);
	}

	void DisableInputs()
	{
		playButton.onButtonClick -= HandlePlayButtonPres;
		exitButton.onButtonClick -= HandleExitButtonPress;
		creditsButton.onButtonClick -= HandleCreditsButtonPress;
	}

}
