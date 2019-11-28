using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    int id;
    [SerializeField]
    private int _speed = 12;
    [SerializeField]
    private int _gravityScale = 25;
    [SerializeField]
    private bool _canMoveHorizontaly;
    private bool _isJumping = false;
    private BoxCollider2D[] _boxColliders;

    private Rigidbody2D _rigidBody;
    private BoxCollider2D _boxCollider2D;

    private bool _lowerBoxCollision;
    private bool _upperBoxCollision;
    private bool _leftBoxCollision;
    private bool _rightBoxCollision;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();

        _boxColliders = GetComponentsInChildren<BoxCollider2D>();

        for (int i = 1; i < _boxColliders.Length; i++)
        {
            Debug.Log(_boxColliders[i].name);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector2 horizontalDirection = new Vector2(horizontalInput, 0);

        //déplacement vers la gauche
        if (horizontalInput < 0)
        {
            _canMoveHorizontaly = CheckCollisionForMovement(-1);
        }

        //déplacement vers la droite
        else if(horizontalInput > 0)
        {
            _canMoveHorizontaly = CheckCollisionForMovement(1);
        }

        if (_canMoveHorizontaly)
        {
            //_rigidBody.MovePosition((Vector2)transform.position + horizontalDirection * Time.deltaTime * _speed);
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * _speed);
        }


        if (Input.GetKeyDown(KeyCode.Space) && _isJumping == false)
        {
            _isJumping = true;
            Jump();
        }
    }

    private void Jump()
    {
        _rigidBody.AddForceAtPosition(transform.up * _gravityScale * _speed, transform.position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            _isJumping = false;
        }
    }

    //Check before moving if the player will collide with an another Object, return if it will or not
    private bool CheckCollisionForMovement(int direction)
    {
        RaycastHit2D[] array = new RaycastHit2D[16];
        Vector2 _CastDirection = new Vector2(direction, 0);

        int nbCollision = _boxCollider2D.Cast(_CastDirection, array, 0.05f);
        if (nbCollision == 0)
        {
            return true;
        }
        else
        {
            return false;
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

