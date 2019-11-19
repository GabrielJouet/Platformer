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

    protected Rigidbody2D _rigidBody2D;



    //Method used to move the enemy at a given position (if one float is given)
    protected void Move(float xPosition)
    {
        //If the new position is on the left
        if (transform.position.x - xPosition < 0f)
        {
            //And we can move left
            if (_canMoveLeft)
            {
                //We move left at a defined speed
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(xPosition, transform.position.y), _speed * Time.deltaTime);
            }
        }
        //Else if the new position is on the right
        else
        {
            //And we can move right
            if (_canMoveRight)
            {
                //We move left at a defined speed
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(xPosition, transform.position.y), _speed * Time.deltaTime);
            }
        }
    }



    //Method used to move the enemy at a given position (if two float are given)
    protected void Move(float xPosition, float yPosition)
    {
        //If the new position is on the left
        if (transform.position.x - xPosition < 0f)
        {
            //We move left at a defined speed
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(xPosition, yPosition), _speed * Time.deltaTime);
        }
        //Else if the new position is on the right
        else
        {
            //We move left at a defined speed
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(xPosition, yPosition), _speed * Time.deltaTime);
        }
    }
}
