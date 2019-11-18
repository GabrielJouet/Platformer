using UnityEngine;

public class WalkingEnemy : MovingEnemy
{
    protected void CheckPosition()
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


    protected Vector2 FindRetreatPosition()
    {
        float newYPosition = transform.position.y;
        float newXPosition;

        if (transform.position.x - _chasingPlayer.transform.position.x < 0f)
        {
            newXPosition = transform.position.x - _minDistanceWithPlayer * 2f;
        }
        else
        {
            newXPosition = transform.position.x + _minDistanceWithPlayer * 2f;
        }

        return new Vector2(newXPosition, newYPosition);
    }
}
