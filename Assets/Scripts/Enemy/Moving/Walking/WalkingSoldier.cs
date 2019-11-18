using UnityEngine;

public class WalkingSoldier : WalkingEnemy
{
    private void Start()
    {
        //We set max speed
        _speedMax = _speed;

        //We launch coroutine for watching player
        StartCoroutine(WatchOutPlayer());
    }


    private void FixedUpdate()
    {
        //If the enemy does already chase someone
        if (_isChasingPlayer)
        {
            //We check the enemy's position
            CheckPosition();


            //If the enemy is too far enough from the player
            if (Mathf.Sqrt((_chasingPlayer.transform.position - transform.position).sqrMagnitude) > _minDistanceWithPlayer + 0.01f)
            {
                //The enemy speeds up
                _speed = _speedMax * 2f;

                //And it moves
                Move(_chasingPlayer.transform.position);
            }
            //If the enemy is too close 
            else if (Mathf.Sqrt((_chasingPlayer.transform.position - transform.position).sqrMagnitude) <= _minDistanceWithPlayer - 0.01f)
            {
                //The enemy speed is reset
                _speed = _speedMax;

                //The enemy can attack the player (close combat only TO CHANGE)
                _isAttackingPlayer = true;

                //And it will moves backward
                Move(FindRetreatPosition());
            }
        }
    }
}
