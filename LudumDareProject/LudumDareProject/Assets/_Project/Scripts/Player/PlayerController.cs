﻿using UnityEngine;
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
    [SerializeField] private float _attackRange = 1;
	
    private Rigidbody2D _RB;

    [SerializeField ] private bool _grounded = true;
    private bool _attacking = false;

    #endregion

    void Awake()
    {
        _RB = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
		DetectInput();

        CheckPosition();
    }

    private void DetectInput()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Jump();
        }
    }

    private void CheckPosition()
    {
        if (transform.position.x <= _xMainPosition)
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

		if (Camera.main.WorldToViewportPoint(transform.position).x < 0)
		{
			//onOutOfScreen();
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

    void OnCollisionEnter2D(Collision2D p_otherCollider)
    {
        if (p_otherCollider.gameObject.CompareTag("Ground"))
        {
            _grounded = true;
        }
    }
}
