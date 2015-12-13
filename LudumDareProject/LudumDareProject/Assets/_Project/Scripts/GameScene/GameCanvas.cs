using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameCanvas : MonoBehaviour 
{
	#region Private Data

	#endregion

	#region Public Data

	public ButtonEffects goButton;

	public ButtonEffects backButton;

	public CanvasGroup onHoldGroup;

	public CanvasGroup statsGroup;

	#endregion

	#region Events

	public Action onGoButtonPress;
	
	public Action onBackButtonPress;

	#endregion

	public void Initialize()
	{
		ListenButtonEvents();
	}

	public void ListenButtonEvents()
	{
		goButton.onButtonClick -= HandleGoButtonPress;
		backButton.onButtonClick -= HandleBackButtonPress; 

		goButton.onButtonClick += HandleGoButtonPress;
		backButton.onButtonClick += HandleBackButtonPress;
	}

	private void HandleGoButtonPress()
	{
		goButton.onButtonClick -= HandleGoButtonPress;

		HideOnHoldGroup(onGoButtonPress);
	}

	private void HandleBackButtonPress()
	{
		backButton.onButtonClick -= HandleBackButtonPress; 

		Action __fadeToMenu = delegate
		{
			FadeCanvas.instance.FadeTo(onBackButtonPress, null);
		};

		HideOnHoldGroup(__fadeToMenu);
	}

	public void ShowOnHoldGroup(Action p_callback)
	{
		ATweenNodule __fadeGroup = ATween.FloatTo(0f, 1f, .6f, Ease.LINEAR, delegate(float p_value) 
		{
			onHoldGroup.alpha = p_value;
		});
		__fadeGroup.onFinished += delegate 
		{
			onHoldGroup.alpha = 1f;
			if (p_callback != null) p_callback();
		};
	}

	public void HideOnHoldGroup(Action p_callback)
	{
		ATweenNodule __fadeGroup = ATween.FloatTo(1f, 0f, .6f, Ease.LINEAR, delegate(float p_value) 
		{
			onHoldGroup.alpha = p_value;
		});
		__fadeGroup.onFinished += delegate 
		{
			onHoldGroup.alpha = 0f;
			if (p_callback != null) p_callback();
		};
	}
}
