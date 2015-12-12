using UnityEngine;
using System.Collections;

public class CoinAction : MonoBehaviour 
{

	#region Private Data

	private ATweenNodule _animationTween;

	#endregion


	void Awake()
	{
		CoinAnimationNormal();
	}

	void CoinAnimationNormal()
	{
		_animationTween = ATween.FloatTo(1f, .1f, .5f, Ease.LINEAR, delegate (float p_value)
		{
			Vector2 __scale = transform.localScale;

			__scale.x = p_value;

			transform.localScale = __scale;
		});
		_animationTween.onFinished += CoinAnimationReverse;
	}

	void CoinAnimationReverse()
	{
		_animationTween = ATween.FloatTo(.1f, 1f, .5f, Ease.LINEAR, delegate (float p_value)
		{
			Vector2 __scale = transform.localScale;

			__scale.x = p_value;

			transform.localScale = __scale;
		});
		_animationTween.onFinished += CoinAnimationNormal;
	}

}
