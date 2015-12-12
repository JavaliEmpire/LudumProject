using UnityEngine;
using System.Collections;

public class Obstacle : Obstacles
{
	#region Private Data

    private float _moveForce = 500f;

	#endregion

    void Awake()
    {
        _RB = this.GetComponent<Rigidbody2D>();

        _renderer = this.GetComponent<Renderer>();
    }

    public override void AUpdate()
    {       
        if (_RB.velocity.x < _speed)
        {
            _RB.AddForce(Vector2.left * _moveForce, ForceMode2D.Force);
        }
        if (Mathf.Abs(_RB.velocity.x) > _speed)
        {
            _RB.velocity = new Vector2(Mathf.Sign(_RB.velocity.x) * _speed, _RB.velocity.y);
        }

        OutOfScreen();
    }

	protected override void OutOfScreen()
    {
        if (Camera.main.WorldToViewportPoint(transform.position).x < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
