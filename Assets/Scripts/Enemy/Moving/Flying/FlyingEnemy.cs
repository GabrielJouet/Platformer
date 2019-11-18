using UnityEngine;

public class FlyingEnemy : MovingEnemy
{
    protected Vector2 FindRetreatPosition()
    {
        float newYPosition;
        float newXPosition;

        if (transform.position.x - _chasingPlayer.transform.position.x < 0f)
        {
            newXPosition = transform.position.x - _minDistanceWithPlayer * 2f;
        }
        else
        {
            newXPosition = transform.position.x + _minDistanceWithPlayer * 2f;
        }

        if (transform.position.y - _chasingPlayer.transform.position.y < 0f)
        {
            newYPosition = transform.position.y - _minDistanceWithPlayer * 2f;
        }
        else
        {
            newYPosition = transform.position.y + _minDistanceWithPlayer * 2f;
        }

        return new Vector2(newXPosition, newYPosition);
    }
}
