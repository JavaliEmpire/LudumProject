using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class FadeCanvas : MonoBehaviour 
{
	#region Static Data

	private static FadeCanvas _instance;
	public static FadeCanvas instance
	{
		get { return _instance; }
	}

	#endregion

	#region Private Data

	private Image _fadePanel;

	private ATweenNodule _fadeTween;

	#endregion

	void Awake()
	{
		_instance = GetComponent<FadeCanvas>();
		_fadePanel = transform.GetChild(0).GetComponent<Image>();
	}


	public void FadeTo(Action p_fadeOutCallback, Action p_fadeInCallback)
	{
		try { _fadeTween.Stop();}
		catch {}

		_fadePanel.gameObject.SetActive(true);

		Color __fadeColor = Color.black;

		_fadeTween = ATween.FloatTo(0f, 1f, .8f, Ease.LINEAR ,delegate(float p_value) 
		{
			__fadeColor.a = p_value;
			_fadePanel.color = __fadeColor;
		});
		_fadeTween.onFinished += delegate 
		{
			__fadeColor.a = 1f;
			_fadePanel.color = __fadeColor;

			if (p_fadeOutCallback != null) p_fadeOutCallback();

			_fadeTween = ATween.FloatTo(1f, 0f, .8f, Ease.LINEAR ,delegate(float p_value) 
			{
				__fadeColor.a = p_value;
				_fadePanel.color = __fadeColor;
			});
			_fadeTween.onFinished += delegate 
			{
				__fadeColor.a = 0f;
				_fadePanel.color = __fadeColor;

				if (p_fadeInCallback != null) p_fadeInCallback();

				_fadePanel.gameObject.SetActive(false);
			};
		};
	}

}
