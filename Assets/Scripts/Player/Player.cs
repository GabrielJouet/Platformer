using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
  // Player speed
  public float _speed = 5f;

  // Can the player jump
  bool _canJump = true;
  // Can the player walljump
  bool _canWallJump = false;
  // Is the player colliding with a wall
  bool[] _collidingSide = new bool[2];
  // Is the player grabbing the wall
  bool _isGrabbingWall = false;

  // Stuck inputs
  bool _inputsStuck = false;

  float _baseGravityScale;

  Rigidbody2D _rb2d;

  // Sprite and animation handling
  SpriteRenderer _spriteRenderer;
  Animator _animator;

  public void AllowJump()
  {
    _animator.SetTrigger("land");
    _canJump = true;
    _canWallJump = true;
  }

  public void DeclareColliding(int id, bool value)
  {
    _canWallJump = true;
    _collidingSide[id] = value;
  }

  void Start()
  {
    _rb2d = GetComponent<Rigidbody2D>();
    _baseGravityScale = _rb2d.gravityScale;

    _spriteRenderer = GetComponent<SpriteRenderer>();
    _animator = GetComponent<Animator>();
  }

  void FixedUpdate()
  {
    _rb2d.gravityScale = _baseGravityScale;

    if (!_canJump && (_collidingSide[0] || _collidingSide[1]))
    {
      if (_rb2d.velocity.y > 0)
      {
        ChangeVerticalSpeed(0f);
      }
      else
      {
        ChangeVerticalSpeed(-.5f);
      }
      _isGrabbingWall = true;
    }
    else
    {
      _isGrabbingWall = false;
    }

    // Handling jump
    if (Input.GetKey("space"))
    {
      if (!_isGrabbingWall)
      {
        if (!_inputsStuck)
        {
          _rb2d.gravityScale = .5f * _baseGravityScale;
        }
      }
      else
      {
        if (_canWallJump)
        {
          StartCoroutine(StuckInputs(.25f));
          _canWallJump = false;

          ChangeVerticalSpeed(0f);
          _rb2d.AddForce(Vector2.up * _rb2d.mass * 700);

          if (_collidingSide[0])
          {
            _rb2d.AddForce(Vector2.left * _rb2d.mass * 400);
            _spriteRenderer.flipX = true;
          }
          else if (_collidingSide[1])
          {
            _rb2d.AddForce(Vector2.right * _rb2d.mass * 400);
            _spriteRenderer.flipX = false;
          }
        }
      }

      if (_canJump && _rb2d.velocity.y >= 0)
      {
        _animator.SetTrigger("jump");
        _canJump = false;
        _rb2d.AddForce(Vector2.up * _rb2d.mass * 500);
      }
    }

    // Handling horizontal movement
    if (!_inputsStuck)
    {
      float horizontalInputValue = Input.GetAxis("Horizontal");

      if (horizontalInputValue > 0)
      {
        _spriteRenderer.flipX = false;
      }
      else if (horizontalInputValue < 0)
      {
        _spriteRenderer.flipX = true;
      }

      Vector2 newVelocity = _rb2d.velocity;
      newVelocity.x = _speed * horizontalInputValue;
      _rb2d.velocity = newVelocity;

      if ( Mathf.Abs(_rb2d.velocity.x) > 0 )
      {
        _animator.SetBool("isRunning", true);
      }
      else
      {
        _animator.SetBool("isRunning", false);
      }
    }
  }

  void ChangeVerticalSpeed(float newSpeed)
  {
    Vector2 newVelocity = _rb2d.velocity;
    newVelocity.y = newSpeed;
    _rb2d.velocity = newVelocity;
  }

  IEnumerator StuckInputs(float time)
  {
    _inputsStuck = true;
    yield return new WaitForSeconds(time);
    _inputsStuck = false;
  }

  public new void GetHit(GameObject emitter, float damage)
  {
    _health -= damage;

    //If we don't have health anymore
    if (_health <= 0f)
    {
      GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
      CameraFollow cameraFollow = camera.GetComponent<CameraFollow>();
      cameraFollow.PlayerDied(emitter);
      Destroy(gameObject);
    }
  }
}
