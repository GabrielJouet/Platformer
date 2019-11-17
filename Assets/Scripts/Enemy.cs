using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField]
    private float _fieldOfSight;


    [SerializeField]
    private Transform _eyePosition;

    [SerializeField]
    private bool _isFlying;


    [SerializeField]
    private List<Checkers> _voidCheckers = new List<Checkers>();


    [SerializeField]
    private float _minDistanceWithPlayer;




    private GameObject _chasingPlayer;

    private bool _isChasingPlayer;


    private bool _isAttackingPlayer;

    private bool _canMoveRight, _canMoveLeft;




    private void Start()
    {
        _speedMax = _speed;

        StartCoroutine(WatchOutPlayer());
    }


    private void FixedUpdate()
    {
        if (_isChasingPlayer)
        {
            CheckPosition();


            if (Mathf.Sqrt((_chasingPlayer.transform.position - transform.position).sqrMagnitude) > _minDistanceWithPlayer + 0.1f)
            {
                Move(_chasingPlayer.transform.position);
            }
            else if(Mathf.Sqrt((_chasingPlayer.transform.position - transform.position).sqrMagnitude) <= _minDistanceWithPlayer - 0.1f)
            {
                _isAttackingPlayer = true;
                Move(FindRetreatPosition());
            }
        }
    }



    private IEnumerator WatchOutPlayer()
    {
        while (true)
        {
            GameObject buffer = GameObject.FindGameObjectWithTag("Player");

            if (buffer != null)
            {
                if (Mathf.Sqrt((buffer.transform.position - transform.position).sqrMagnitude) < _fieldOfSight)
                {
                    if (Physics2D.Linecast(_eyePosition.position, buffer.transform.position).collider == buffer.GetComponent<CircleCollider2D>())
                    {
                        _isChasingPlayer = true;
                        _chasingPlayer = buffer;
                    }
                    else
                    {
                        _isChasingPlayer = false;
                    }
                }
            }

            yield return new WaitForSecondsRealtime(0.2f);
        }
    }


    private void CheckPosition()
    {
        if(!_isFlying)
        {
            if (_voidCheckers[0].GetIsTouchingWall())
            {
                _canMoveRight = true;
            }
            else
            {
                _canMoveRight = false;
            }

            if (_voidCheckers[1].GetIsTouchingWall())
            {
                _canMoveLeft = true;
            }
            else
            {
                _canMoveLeft = false;
            }
        }
    }


    private void Move(Vector3 newPosition)
    {
        if(transform.position.x - newPosition.x < 0f)
        {
            if(_canMoveLeft)
            {
                transform.position = Vector3.MoveTowards(transform.position, newPosition, _speed * Time.deltaTime);
            }
        }
        else
        {
            if(_canMoveRight)
            {
                transform.position = Vector3.MoveTowards(transform.position, newPosition, _speed * Time.deltaTime);
            }
        }
    }


    private Vector2 FindRetreatPosition()
    {
        float newYPosition = transform.position.y;
        float newXPosition;

        if(transform.position.x - _chasingPlayer.transform.position.x < 0f)
        {
            newXPosition = transform.position.x - _minDistanceWithPlayer;
        }
        else
        {
            newXPosition = transform.position.x + _minDistanceWithPlayer;
        }

        if(_isFlying)
        {
            if(transform.position.y - _chasingPlayer.transform.position.y < 0f)
            {
                newYPosition = transform.position.y - _minDistanceWithPlayer;
            }
            else
            {
                newYPosition = transform.position.y + _minDistanceWithPlayer;
            }
        }

        return new Vector2(newXPosition, newYPosition);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            _isGrounded = true;
        }
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            _isGrounded = false;
        }
    }
}
