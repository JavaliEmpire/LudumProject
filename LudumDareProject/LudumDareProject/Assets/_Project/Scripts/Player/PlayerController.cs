using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
    #region Public Data

    public GameObject weapon;

    #endregion

    #region Private Data

    [SerializeField] private float _xMainPosition = 0;
    [SerializeField] private float _maxSpeed = 1;
    [SerializeField] private float _moveForce = 20;
    [SerializeField] private float _jumpForce = 440f;
	
    private Rigidbody2D _RB;

    [SerializeField] private bool _grounded = true;
    [SerializeField] private bool _obstacleGrounded = false;
    [SerializeField] private bool _beingPushed = false;
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

    private void CheckPosition()
    {
        if (Camera.main.WorldToViewportPoint(transform.position).x < 0)
        {
            StateMachine.ChangeState(StateMachine.StateType.MENU);
        }
        if (_beingPushed/*&& _obstacleGrounded*/)
        {
            return;
        }

        if (transform.position.x < _xMainPosition)
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

    void OnCollisionEnter2D(Collision2D p_other)
    {
        if (p_other.gameObject.CompareTag("Ground"))
        {
            _grounded = true;
        }
    }

    void OnCollisionStay2D(Collision2D p_other)
    {

    }

    void OnCollisionExit2D(Collision2D p_other)
    {
        if (p_other.gameObject.CompareTag("Ground"))
        {
            _grounded = false;
        }

    }

}
