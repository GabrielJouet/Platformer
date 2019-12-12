using UnityEngine;

public class WalkingSoldier : WalkingEnemy, IPatrollable
{
    private int _patrolState;
    public int patrolState
    {
        get
        {
            return this._patrolState;
        }
        set
        {
            this._patrolState = value;
        }
    }

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
            CheckGroundedPosition();

            float distanceWithPlayer = Mathf.Sqrt(Mathf.Pow(_chasingPlayer.transform.position.x - transform.position.x, 2));


            //If the enemy is too far enough from the player
            if (distanceWithPlayer > _minDistanceWithPlayer + _speed * Time.fixedDeltaTime)
            {
                //The enemy speeds up
                _speed = _speedMax * 2f;

                //And it moves
                Move(_chasingPlayer.transform.position.x);
            }
            //If the enemy is too close 
            else if (distanceWithPlayer <= _minDistanceWithPlayer - _speed * Time.fixedDeltaTime)
            {
                //The enemy speed is reset
                _speed = _speedMax;

                //The enemy can attack the player (close combat only TO CHANGE)
                _isAttackingPlayer = true;

                //And it will moves backward
                Move(FindRetreatPosition().x);
            }
        }
        else
        {
            // TODO patrol state
        }
    }
}
