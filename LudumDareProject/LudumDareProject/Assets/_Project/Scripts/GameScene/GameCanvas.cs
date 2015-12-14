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

	public Text goldText;

	public Text expText;

	public Image expBar;

	public Text stageText;

	#endregion

	#region Events

	public Action onGoButtonPress;
	
	public Action onBackButtonPress;

	#endregion

	public void Initialize()
	{
		RefreshStats();

		ListenButtonEvents();
		
		GameModel.instance.refreshStatsAction += RefreshStats;
	}

	public void ListenButtonEvents()
	{
		DisableInputs();

		goButton.onButtonClick += HandleGoButtonPress;
		backButton.onButtonClick += HandleBackButtonPress;
	}

	void DisableInputs()
	{
		goButton.onButtonClick -= HandleGoButtonPress;
		backButton.onButtonClick -= HandleBackButtonPress;
	}

	public void RefreshStats()
	{
		int __gold = GameModel.instance.dictData[GameModel.DataType.GOLD.ToString()];

		int __characterLevel = GameModel.instance.dictData[GameModel.DataType.CHARACTER_LEVEL.ToString()];

		int __characterCurrentExperience = GameModel.instance.dictData[GameModel.DataType.CHARACTER_EXPERIENCE.ToString()];

		int __characterNextLevelExperiente = Mathf.CeilToInt(Mathf.Pow((float)__characterLevel, 2f) * 10);

		stageText.text = "" + __characterLevel;

		goldText.text = "x " + __gold;

		expText.text = __characterCurrentExperience + " / " + __characterNextLevelExperiente;

		expBar.fillAmount = (float)__characterCurrentExperience / (float)__characterNextLevelExperiente;
	}

	private void HandleGoButtonPress()
	{
		DisableInputs();

		HideOnHoldGroup(onGoButtonPress);
	}

	private void HandleBackButtonPress()
	{
		DisableInputs();

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
