using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : Enemy
{
    //Actual speed of the enemy
    [SerializeField]
    protected float _speed;


    //Maximum speed of the enemy
    protected float _speedMax;


    //Two checkers used to check if a collision occurs 
    [SerializeField]
    protected List<Checker> _voidCheckers = new List<Checker>();

    //Did the enemy can move right or left?
    protected bool _canMoveRight, _canMoveLeft;



    //Method used to move the enemy at a given position
    protected void Move(Vector3 newPosition)
    {
        //If the new position is on the left
        if (transform.position.x - newPosition.x < 0f)
        {
            //And we can move left
            if (_canMoveLeft)
            {
                //We move left at a defined speed
                transform.position = Vector3.MoveTowards(transform.position, newPosition, _speed * Time.deltaTime);
            }
        }
        //Else if the new position is on the right
        else
        {
            //And we can move right
            if (_canMoveRight)
            {
                //We move left at a defined speed
                transform.position = Vector3.MoveTowards(transform.position, newPosition, _speed * Time.deltaTime);
            }
        }
    }
}
