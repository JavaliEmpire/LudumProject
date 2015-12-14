using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Background : MonoBehaviour
{
    #region Public Data

	public OffsetScroller[] parallaxes;

    #endregion

	public void SetMultiplier(float p_multiplier)
	{
		float __currentMultiplier = parallaxes[0].scrollSpeedMultiplier;

		float __multiplierBoost = p_multiplier - __currentMultiplier;


		ATween.FloatTo(0f, 1f, 1f, Ease.LINEAR, delegate(float p_value) 
		{
			for (int i = 0; i < parallaxes.Length; i++)
			{
				parallaxes[i].scrollSpeedMultiplier = __currentMultiplier + __multiplierBoost * p_value;
			}
		});
	}
}

