using UnityEngine;

public class FlyingEnemy : MovingEnemy
{
    //Method used to get back when the player is too close
    protected Vector2 FindRetreatPosition()
    {
        float newYPosition;
        float newXPosition;


        //If the player is on the right
        if (transform.position.x - _chasingPlayer.transform.position.x < 0f)
        {
            //We go back (on the left)
            newXPosition = transform.position.x - _minDistanceWithPlayer;
        }
        //If the player is on the left
        else
        {
            //We go back (on the right)
            newXPosition = transform.position.x + _minDistanceWithPlayer;
        }


        //If the player is up
        if (transform.position.y - _chasingPlayer.transform.position.y < 0f)
        {
            //We go back (down)
            newYPosition = transform.position.y - _minDistanceWithPlayer;
        }
        //If the player is down
        else
        {
            //We go back (up)
            newYPosition = transform.position.y + _minDistanceWithPlayer;
        }


        return new Vector2(newXPosition, newYPosition);
    }
}
