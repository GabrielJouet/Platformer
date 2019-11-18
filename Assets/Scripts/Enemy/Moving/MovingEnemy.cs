using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : Enemy
{
    [SerializeField]
    protected float _speed;


    protected float _speedMax;


    [SerializeField]
    protected List<Checker> _voidCheckers = new List<Checker>();

    protected bool _canMoveRight, _canMoveLeft;


    protected void Move(Vector3 newPosition)
    {
        if (transform.position.x - newPosition.x < 0f)
        {
            if (_canMoveLeft)
            {
                transform.position = Vector3.MoveTowards(transform.position, newPosition, _speed * Time.deltaTime);
            }
        }
        else
        {
            if (_canMoveRight)
            {
                transform.position = Vector3.MoveTowards(transform.position, newPosition, _speed * Time.deltaTime);
            }
        }
    }
}
