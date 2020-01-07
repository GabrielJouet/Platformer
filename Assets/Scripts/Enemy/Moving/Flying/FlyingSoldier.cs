using System.Collections;
using UnityEngine;

public class FlyingSoldier : FlyingEnemy, IShootable
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
	[SerializeField]
	protected float _shotRange;
	public float shotRange
	{
		get
		{
			return this._shotRange;
		}
		set
		{
			this._shotRange = value;
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

	private void Update()
	{
		if (_chasingPlayer != null && _isAttackingPlayer && _canShoot)
		{
			AttackPlayer();
		}
	}

	private void FixedUpdate()
	{
		//If the enemy does already chase someone
		if (_chasingPlayer != null)
		{
			float distanceWithPlayer = Mathf.Sqrt((transform.position - _chasingPlayer.transform.position).sqrMagnitude);

			//If the enemy is too far enough from the player
			if (distanceWithPlayer > _minDistanceWithPlayer + 0.01f)
			{
				_isAttackingPlayer = false;
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

				Vector2 retreatPosition = FindRetreatPosition();

				//And it will moves backward
				Move(retreatPosition.x, retreatPosition.y);
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