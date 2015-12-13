using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
    #region Public Data

    public GameObject weapon;

	public Action<Action> onOutOfScreen;

    #endregion

    #region Private Data

	private Vector3 _initialPosition = new Vector3(-4f, -.9f, 0f);
	
    [SerializeField] private float _maxSpeed = 1;
    [SerializeField] private float _moveForce = 20;
    [SerializeField] private float _jumpForce = 440f;
	
    private Rigidbody2D _RB;

    private bool _grounded = true;

    private bool _attacking = false;

    #endregion

    void Awake()
    {
        _RB = this.GetComponent<Rigidbody2D>();
    }

    public void AUpdate()
    {
		DetectInput();

        CheckPosition();
    }

    private void DetectInput()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Jump();
        }
    }

	public void ResetPosition()
	{
		transform.position = _initialPosition;
	}

    private void CheckPosition()
    {
        if (Camera.main.WorldToViewportPoint(transform.position).x < 0)
        {
			if (onOutOfScreen != null) onOutOfScreen(ResetPosition);
        }

		Vector3 __screenCenter = Vector3.one / 2;

		__screenCenter = Camera.main.ViewportToWorldPoint(__screenCenter);

		if (transform.position.x < __screenCenter.x)
        {
            if (_RB.velocity.x < _maxSpeed)
            {
                _RB.AddForce(Vector2.right * _moveForce,ForceMode2D.Force);
            }
            if (Mathf.Abs(_RB.velocity.x) > _maxSpeed)
            {
                _RB.velocity = new Vector2(Mathf.Sign(_RB.velocity.x) * _maxSpeed, _RB.velocity.y);
            }
        }
    }

    private void Jump()
    {
        if (_grounded == false) return;

		_grounded = false;

        _RB.AddForce(Vector2.up * _jumpForce, ForceMode2D.Force);
    }

    private void Attack()
    {
        if (_attacking == true) return;
        _attacking = true;

        weapon.SetActive(_attacking);

        ATimer.WaitSeconds(0.1f, delegate
        {
            _attacking = false;
            weapon.SetActive(_attacking);
        });
    }

    void OnCollisionStay2D(Collision2D p_other)
    {
		if (p_other.gameObject.CompareTag("Ground"))
        {
            _grounded = true;
        }
    }
	
    void OnCollisionExit2D(Collision2D p_other)
    {
        if (p_other.gameObject.CompareTag("Ground"))
        {
            	_grounded = false;
        }
    }
	

}
