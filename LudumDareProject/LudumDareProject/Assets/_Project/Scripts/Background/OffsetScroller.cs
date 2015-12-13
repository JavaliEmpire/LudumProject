using UnityEngine;
using System.Collections;

public class OffsetScroller : MonoBehaviour
{

    public float scrollSpeed;
    private Vector2 savedOffset;
    private Renderer _renderer;

	public bool active;

    void Start()
    {
        _renderer = this.GetComponent<Renderer>();
        savedOffset = _renderer.sharedMaterial.GetTextureOffset("_MainTex");
    }

    void Update()
    {
		if (active == true)
		{
	        float __x = Mathf.Repeat(Time.time * scrollSpeed, 1);
	        Vector2 __offset = new Vector2(__x, savedOffset.y);
	        _renderer.sharedMaterial.SetTextureOffset("_MainTex", __offset);
		}
    }

    void OnDisable()
    {
        _renderer.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
    }
}