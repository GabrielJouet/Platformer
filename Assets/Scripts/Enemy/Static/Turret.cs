using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy, IShootable
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

	protected Transform barrelTransform;

	protected float maxAngle = 45.0f;
	protected bool canShoot = true;

	public IEnumerator StartCooldown()
	{
		this.canShoot = false;
		yield return new WaitForSecondsRealtime(shotCooldown);
		this.canShoot = true;
	}

	public void AttackPlayer()
	{
		Projectile buffer = projectilePool.UseProjectile(gameObject, bulletId);

		buffer.Restart();
		buffer.transform.position = transform.position;
		buffer.transform.up = barrelTransform.up;
		buffer.transform.Rotate(buffer.transform.forward, Random.Range(-shotPrecision, shotPrecision));

		StartCoroutine("StartCooldown");
	}
}
