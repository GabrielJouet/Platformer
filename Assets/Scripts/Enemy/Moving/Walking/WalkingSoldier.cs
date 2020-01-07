using System.Collections;
using UnityEngine;

public class WalkingSoldier : WalkingEnemy, IPatrollable, IShootable
{
    [SerializeField]
    protected int _bulletId;
    public int bulletId
    {
        get
        {
            return this._bulletId;
        }
        set
        {
            this._bulletId = value;
        }
    }
    [SerializeField]
    protected float _shotCooldown;
    public float shotCooldown
    {
        get
        {
            return this._shotCooldown;
        }
        set
        {
            this._shotCooldown = value;
        }
    }
    [SerializeField]
    protected float _shotPrecision;
    public float shotPrecision
    {
        get
        {
            return this._shotPrecision;
        }
        set
        {
            this._shotPrecision = value;
        }
    }

    protected ProjectilePool _projectilePool;
    public ProjectilePool projectilePool
    {
        get
        {
            return this._projectilePool;
        }
        set
        {
            this._projectilePool = value;
        }
    }

    protected bool _canShoot = true;

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
        _projectilePool = FindObjectOfType<ProjectilePool>();

        //We set max speed
        _speedMax = _speed;

        patrolState = 1;

        patrolStartingPoint = transform.position.x;

        //We launch coroutine for watching player
        StartCoroutine(WatchOutPlayer());
    }

    private void Update()
    {
        if (_isAttackingPlayer && _canShoot)
        {
            AttackPlayer();
        }
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
                _isAttackingPlayer = false;

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
            _speed = _speedMax;

            CheckGroundedPosition();

            ProcessPatrol();
        }
    }

    private void ProcessPatrol()
    {
        if (isPatrolLimited)
        {
            switch (patrolState)
            {
                case 0:
                    if (!_stillRememberPlayer)
                    {
                        patrolState = 1;
                    }
                    break;
                case 1:
                    if (_canMoveLeft && transform.position.x + _speed * Time.deltaTime < patrolStartingPoint + patrolMagnitude / 2)
                    {
                        Move(transform.position.x + _speed);
                    }
                    else
                    {
                        patrolState = 2;
                    }
                    break;
                case 2:
                    if (_canMoveRight && transform.position.x - _speed * Time.deltaTime > patrolStartingPoint - patrolMagnitude / 2)
                    {
                        Move(transform.position.x - _speed);
                    }
                    else
                    {
                        patrolState = 1;
                    }
                    break;
            }
        }
        else
        {
            switch (patrolState)
            {
                case 0:
                    if (!_stillRememberPlayer)
                    {
                        patrolState = 1;
                    }
                    break;
                case 1:
                    if (_canMoveLeft )
                    {
                        Move(transform.position.x + _speed);
                    }
                    else
                    {
                        patrolState = 2;
                    }
                    break;
                case 2:
                    if (_canMoveRight)
                    {
                        Move(transform.position.x - _speed);
                    }
                    else
                    {
                        patrolState = 1;
                    }
                    break;
            }
        }
    }

    public IEnumerator StartCooldown()
    {
        this._canShoot = false;
        yield return new WaitForSecondsRealtime(shotCooldown);
        this._canShoot = true;
    }

    public void AttackPlayer()
    {
        Projectile buffer = projectilePool.UseProjectile(bulletId);

        buffer.Restart();
        buffer.transform.position = transform.position;
        buffer.transform.up = (_chasingPlayer.transform.position - transform.position).normalized;
        buffer.transform.Rotate(buffer.transform.forward, Random.Range(-shotPrecision, shotPrecision));

        StartCoroutine("StartCooldown");
    }
}