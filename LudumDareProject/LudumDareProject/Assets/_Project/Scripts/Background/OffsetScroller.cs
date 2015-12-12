using UnityEngine;
using System.Collections;

public class OffsetScroller : MonoBehaviour
{

    public float scrollSpeed;
    private Vector2 savedOffset;
    private Renderer _renderer;

    void Start()
    {
        _renderer = this.GetComponent<Renderer>();
        savedOffset = _renderer.sharedMaterial.GetTextureOffset("_MainTex");
    }

    void Update()
    {
        float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(x, savedOffset.y);
        _renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    void OnDisable()
    {
        _renderer.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
    }
}