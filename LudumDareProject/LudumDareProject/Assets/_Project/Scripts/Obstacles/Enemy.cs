using UnityEngine;
using System.Collections;

public class Enemy : Obstacles
{
    #region Private Data

    [SerializeField]
    private int _maxLife;

    private int _currentLife;


    #endregion

    void Awake()
    {
        _currentLife = _maxLife;
        _moveForce = 500f;
        _RB = this.GetComponent<Rigidbody2D>();
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

    private void TakeDamage(int p_damage)
    {
        _currentLife -= p_damage;

        if (_currentLife <= 0)
        {
			int __currentLevel = GameModel.instance.dictData[GameModel.DataType.CHARACTER_LEVEL.ToString()];

			int __goldMultiplier = GameModel.instance.dictData[GameModel.DataType.GOLD_MULT.ToString()];

			GameModel.instance.dictData[GameModel.DataType.GOLD.ToString()] += __currentLevel * __goldMultiplier;

			GameModel.instance.AddExp(__currentLevel);

			GameModel.instance.refreshStatsAction();
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D p_other)
    {
        if (p_other.CompareTag("Weapon"))
        {
            TakeDamage(p_other.GetComponent<Weapon>().damage);
        }
    }

    protected override void OutOfScreen()
    {
        if (Camera.main.WorldToViewportPoint(transform.position).x < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
