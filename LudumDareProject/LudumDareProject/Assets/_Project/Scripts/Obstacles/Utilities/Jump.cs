using UnityEngine;
using System.Collections;

public class Jump : ObstacleUtility
{
    [SerializeField] float _jumpForce = 50;
    void Awake()
    {
        _RB = this.GetComponent<Rigidbody2D>();
        _moveForce = 500f;
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
    }

    void OnCollisionEnter2D(Collision2D p_other)
    {
        if (p_other.gameObject.CompareTag("Player"))
        {
            p_other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
}
