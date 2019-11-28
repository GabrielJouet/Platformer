using System.Collections;
using UnityEngine;

public class FlyingSoldier : FlyingEnemy
{
    [SerializeField]
    private Projectile _currentProjectile;


    [SerializeField]
    private float _timeBetweenShots;


    private ProjectilePool _projectilePool;



    private void Start()
    {
        //We set max speed
        _speedMax = _speed;

        _rigidBody2D = GetComponent<Rigidbody2D>();

        _rigidBody2D.gravityScale = 0f;

        _projectilePool = FindObjectOfType<ProjectilePool>();

        //We launch coroutine for watching player
        StartCoroutine(WatchOutPlayer());
    }


    private void FixedUpdate()
    {
        //If the enemy does already chase someone
        if (_isChasingPlayer)
        {
            float distanceWithPlayer = Mathf.Sqrt((transform.position - _chasingPlayer.transform.position).sqrMagnitude);


            //If the enemy is too far enough from the player
            if (distanceWithPlayer > _minDistanceWithPlayer + 0.01f)
            {
                //The enemy speeds up
                _speed = _speedMax * 2f;

                //And it moves
                Move(_chasingPlayer.transform.position.x, _chasingPlayer.transform.position.y);
            }
            //If the enemy is too close 
            else if (distanceWithPlayer <= _minDistanceWithPlayer - 0.01f)
            {
                //The enemy speed is reset
                _speed = _speedMax;

                //The enemy can attack the player (close combat only TO CHANGE)
                _isAttackingPlayer = true;

                StartCoroutine(ShootPlayer());

                Vector2 retreatPosition = FindRetreatPosition();

                //And it will moves backward
                Move(retreatPosition.x, retreatPosition.y);
            }
        }
    }


    private IEnumerator ShootPlayer()
    {
        while(true)
        {
            Projectile buffer = _projectilePool.UseProjectile(_currentProjectile);
            buffer.Restart();

            buffer.transform.position = transform.position;

            yield return new WaitForSecondsRealtime(_timeBetweenShots);
        }
    }
}
