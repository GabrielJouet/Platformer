using UnityEngine;

public class WalkingEnemy : MovingEnemy
{
    //Method used to check actual platform (if we are on the edge)
    protected void CheckGroundedPosition()
    {
        //If the right checker still collide
        if (_voidCheckers[1].GetIsTouchingWall())
        {
            _canMoveRight = true;
        }
        //If the right checker didn't find wall
        else
        {
            _canMoveRight = false;
        }


        //If the left checker still collide
        if (_voidCheckers[0].GetIsTouchingWall())
        {
            _canMoveLeft = true;
        }
        //If the left checker didn't find wall
        else
        {
            _canMoveLeft = false;
        }
    }


    //Method used to find a retreat position
    protected Vector2 FindRetreatPosition()
    {
        float newXPosition;

        //If the player is on the left
        if (transform.position.x - _chasingPlayer.transform.position.x < 0f)
        {
            //The enemy goes right
            newXPosition = transform.position.x - _minDistanceWithPlayer;
        }
        //If the player is on the right
        else
        {
            //The enemy goes left
            newXPosition = transform.position.x + _minDistanceWithPlayer;
        }


        return new Vector2(newXPosition, transform.position.y);
    }
}
