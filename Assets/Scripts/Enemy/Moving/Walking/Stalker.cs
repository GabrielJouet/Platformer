using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalker : WalkingEnemy, IShootable
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

  private bool _isHiding = true;
	private SpriteRenderer _spriteRenderer;

  private void Start()
  {
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _projectilePool = FindObjectOfType<ProjectilePool>();
    _speedMax = _speed;
    StartCoroutine(WatchOutPlayer());
  }

  private void Update()
  {
    if (_chasingPlayer != null)
    {
      CheckGroundedPosition();
      float distanceWithPlayer = Mathf.Sqrt(Mathf.Pow(_chasingPlayer.transform.position.x - transform.position.x, 2));

      if (_isAttackingPlayer && _canShoot)
      {
        AttackPlayer();
      }

      if (distanceWithPlayer > _minDistanceWithPlayer + _speed * Time.fixedDeltaTime)
      {
          _speed = _speedMax * 2f;
          Move(_chasingPlayer.transform.position.x);
      }
      else if (distanceWithPlayer <= _minDistanceWithPlayer - _speed * Time.fixedDeltaTime)
      {
          _speed = _speedMax;
          Move(FindRetreatPosition().x);
      }

      if (distanceWithPlayer < _shotRange)
      {
          _isAttackingPlayer = true;
      }
      else
      {
          _isAttackingPlayer = false;
      }
    }
  }

  void FixedUpdate()
  {
    if (_chasingPlayer != null)
    {
      _isHiding = false;
    }
    else if (!_stillRememberPlayer)
    {
      _isHiding = true;
    }

    if (_isHiding && _spriteRenderer.enabled)
    {
      _spriteRenderer.enabled = false;
    }
    if (!_isHiding && !_spriteRenderer.enabled)
    {
      _spriteRenderer.enabled = true;
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
      Projectile buffer = projectilePool.UseProjectile(gameObject, bulletId);

      buffer.Restart();
      buffer.transform.position = transform.position;
      buffer.transform.up = (_chasingPlayer.transform.position - transform.position).normalized;
      buffer.transform.Rotate(buffer.transform.forward, Random.Range(-shotPrecision, shotPrecision));

      StartCoroutine("StartCooldown");
  }
}
