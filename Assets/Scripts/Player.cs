using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _speed = 12;
    [SerializeField]
    private int _gravityScale = 25;
    [SerializeField]
    private bool _isJumping = false;
    private bool _isFalling = false;
    private bool _hitGround = false;
    private bool _isRunning = false;
    private bool _jumpIsOver = true;

    private bool _facingRight = true;

    private Rigidbody2D _rigidBody;
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private bool _lowerBoxCollision = false;
    [SerializeField]
    private bool _upperBoxCollision = false;
    [SerializeField]
    private bool _leftBoxCollision = false;
    [SerializeField]
    private bool _rightBoxCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float direction = Input.GetAxis("Horizontal");
        if (direction > 0f && !_rightBoxCollision)
        {
            Flip(direction);
            transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.fixedDeltaTime * _speed);          
        }
        else if (direction < 0f && !_leftBoxCollision)
        {
            Flip(direction);
            transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.fixedDeltaTime * _speed);
        }

        if (Input.GetKeyDown(KeyCode.Space) && ((_lowerBoxCollision && _rightBoxCollision) || (_lowerBoxCollision && _leftBoxCollision) || _rightBoxCollision || _leftBoxCollision || _lowerBoxCollision))
        {
            Jump();
        }

        CheckAnimation(direction);
    }

    private void Jump()
    {
        _rigidBody.AddForceAtPosition(transform.up * _gravityScale * _speed, transform.position);
        _isJumping = true;
        _jumpIsOver = false;
        this._animator.SetBool("isJumping", _isJumping);
    }

    //Flips the player depending on it's horizontal direction
    private void Flip(float direction)
    {
        if (direction > 0 && !_facingRight)
        {
            _facingRight = !_facingRight;
            _spriteRenderer.flipX = false;
        }
        if (direction < 0 && _facingRight)
        {
            _facingRight = !_facingRight;
            _spriteRenderer.flipX = true;
        }
    }

    //Animation handler
    private void CheckAnimation(float direction)
    {
        _hitGround = false;
        this._animator.SetBool("hitGround", _hitGround);
        if (this._rigidBody.velocity.y < -0.2f )
        {
            _isJumping = false;
            this._animator.SetBool("isJumping", _isJumping);
            _isFalling = true;
            this._animator.SetBool("isFalling", _isFalling);
        }
        if (_lowerBoxCollision && _isFalling)
        {
            _isFalling = false;
            this._animator.SetBool("isFalling", _isFalling);
            _hitGround = true;
            this._animator.SetBool("hitGround", _hitGround);
        }

        if (direction >= 0.5f || direction <= -0.5f)
        {
            _isRunning = true;
            this._animator.SetBool("isRunning", _isRunning);
        }
        else if(direction <= 0.5f && direction >= -0.5f)
        {
            _isRunning = false;
            this._animator.SetBool("isRunning", _isRunning);
        }
    }

    public void SetLowerBoxCollision(bool boolean)
    {
        _lowerBoxCollision = boolean;
    }
    public void SetUpperBoxCollision(bool boolean)
    {
        _upperBoxCollision = boolean;
    }
    public void SetLeftBoxCollision(bool boolean)
    {
        _leftBoxCollision = boolean;
    }
    public void SetRightBoxCollision(bool boolean)
    {
        _rightBoxCollision = boolean;
    }
}

