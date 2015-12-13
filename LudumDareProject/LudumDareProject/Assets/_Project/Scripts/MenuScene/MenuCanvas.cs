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

	void Awake()
	{
		playButton.onButtonClick += delegate 
		{
			FadeCanvas.instance.FadeTo(delegate 
			{
				StateMachine.ChangeState(StateMachine.StateType.GAME);
			}, null);
		};

		exitButton.onButtonClick += delegate 
		{
			FadeCanvas.instance.FadeTo(delegate 
			{
				Application.Quit();
			}, null);
		};

		creditsButton.onButtonClick += delegate
		{
			FadeCanvas.instance.FadeTo(delegate 
			{
				StateMachine.ChangeState(StateMachine.StateType.CREDITS);
			}, null);
		};
	}


}
