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
    private bool _canMoveHorizontaly;
    //private bool _isJumping = false;

    private Rigidbody2D _rigidBody;
    private BoxCollider2D _boxCollider2D;

    private bool _lowerBoxCollision = false;
    private bool _upperBoxCollision = false;
    private bool _leftBoxCollision = false;
    private bool _rightBoxCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
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


        if (Input.GetKeyDown(KeyCode.Space) && ((_lowerBoxCollision && _rightBoxCollision) || (_lowerBoxCollision && _leftBoxCollision) || _rightBoxCollision || _leftBoxCollision || _lowerBoxCollision))
        {
            Jump();
        }
    }

    private void Jump()
    {
        _rigidBody.AddForceAtPosition(transform.up * _gravityScale * _speed, transform.position);
    }

    /*
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            _isJumping = false;
        }
    }*/

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
        Debug.Log("lower :" + _lowerBoxCollision);
    }
    public void SetUpperBoxCollision(bool boolean)
    {
        _upperBoxCollision = boolean;
        Debug.Log("upper :" + _upperBoxCollision);
    }
    public void SetLeftBoxCollision(bool boolean)
    {
        _leftBoxCollision = boolean;
        Debug.Log("left :" + _leftBoxCollision);
    }

    public void SetRightBoxCollision(bool boolean)
    {
        _rightBoxCollision = boolean;
        Debug.Log("right :" + _rightBoxCollision);
    }
}

