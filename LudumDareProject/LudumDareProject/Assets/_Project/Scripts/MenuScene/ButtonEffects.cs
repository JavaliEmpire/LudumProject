using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;

public class ButtonEffects : MonoBehaviour 
{
	#region Private Data

	private ATweenNodule _animationTween;

	private EventTrigger _es;

	private Image _image;

	private float _initialAlpha;

	#endregion

	#region Public Data

	public Action onButtonClick;

	#endregion


	void Awake () 
	{
		_image = GetComponent<Image>();
		_es = GetComponent<EventTrigger>();

		_initialAlpha = _image.color.a;
	}

	public void OnPointerEnter()
	{
		try { _animationTween.Stop(); }
		catch {}

		Color __imageColor = _image.color;

		float __animationTimer = 1f - __imageColor.a;

		_animationTween = ATween.FloatTo(__imageColor.a, 1f, __animationTimer, Ease.LINEAR, delegate (float p_value)
		{
			__imageColor.a = p_value;
			_image.color = __imageColor;
		});

	}

	public void OnPointerExit()
	{
		try { _animationTween.Stop(); }
		catch {}
		
		Color __imageColor = _image.color;
		
		float __animationTimer = Mathf.Abs(__imageColor.a - _initialAlpha);
		
		_animationTween = ATween.FloatTo(__imageColor.a, _initialAlpha, __animationTimer, Ease.LINEAR, delegate (float p_value)
		{
			__imageColor.a = p_value;
			_image.color = __imageColor;
		});
	}

	public void OnPointerClick()
	{
		if (onButtonClick != null) onButtonClick();
	}
}
