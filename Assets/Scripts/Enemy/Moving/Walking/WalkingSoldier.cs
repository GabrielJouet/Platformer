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
    [SerializeField]
    private bool _isPatrolLimited;
    public bool isPatrolLimited
    {
        get
        {
            return this._isPatrolLimited;
        }
        set
        {
            this._isPatrolLimited = value;
        }
    }
    [SerializeField]
    private float _patrolMagnitude;
    public float patrolMagnitude
    {
        get
        {
            return this._patrolMagnitude;
        }
        set
        {
            this._patrolMagnitude = value;
        }
    }

    private float patrolStartingPoint;

    private void Start()
    {
        //We set max speed
        _speedMax = _speed;

        patrolState = 1;

        patrolStartingPoint = transform.position.x;

        //We launch coroutine for watching player
        StartCoroutine(WatchOutPlayer());
    }


    private void FixedUpdate()
    {
        //If the enemy does already chase someone
        if (_isChasingPlayer)
        {
            patrolState = 0;

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
            /*
             * This demonstrates the way to use the patrollable interface
             * In the patrol state 1 (when the enemy goes right), he's stuck by his patrol magnitude
             * But when he goes left, he just try to go as far as possible to the left
             */

            CheckGroundedPosition();

            if (patrolState == 1)
            {
                if (transform.position.x + _speed * Time.deltaTime < patrolStartingPoint + patrolMagnitude / 2)
                {
                    Move(transform.position.x + _speed);
                }
                else
                {
                    patrolState = 2;
                }
            }
            else if (patrolState == 2)
            {
                if (_canMoveRight)
                {
                    Move(transform.position.x - _speed);
                }
                else
                {
                    patrolState = 1;
                }
            }
        }
    }
}
