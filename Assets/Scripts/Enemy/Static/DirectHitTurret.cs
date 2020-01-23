using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectHitTurret : Turret
{
	[SerializeField]
	protected float _burstCooldown;
	[SerializeField]
	protected int _burstLength;
	protected bool _canShoot = true;
	protected bool _lockBarrel = false;

	private void Start()
	{
		barrelTransform = transform.GetChild(0);
		projectilePool = FindObjectOfType<ProjectilePool>();

		StartCoroutine("WatchOutPlayer");
	}

	private void Update()
	{
		// If the watching coroutine detect that the turret is seeing the player
		if (_chasingPlayer != null)
		{
			// The turret rotate to the direction of the player if the angle is small enough
			Vector2 PlayerOffset;

			PlayerOffset = _chasingPlayer.transform.position - transform.position;
			PlayerOffset.Normalize();

			if (Vector2.Angle(transform.up, PlayerOffset) < maxAngle)
			{
				// And start to attack
				_isAttackingPlayer = true;

				if (!_lockBarrel)
				{
					barrelTransform.up = PlayerOffset;
				}

				if (_canShoot)
				{
					_lockBarrel = true;
					AttackPlayer();
				}
			}
		}
	}

	public void AttackPlayer()
	{
		Vector3 direction = barrelTransform.up;

		StartCoroutine(ShootBurst(direction));
	}

	void ShootProjectile(Vector3 direction)
	{
		Projectile buffer = projectilePool.UseProjectile(gameObject, bulletId);

		buffer.Restart();
		buffer.transform.position = transform.position;
		buffer.transform.up = direction;
		buffer.transform.Rotate(buffer.transform.forward, Random.Range(-shotPrecision, shotPrecision));
	}

	IEnumerator ShootBurst(Vector3 direction)
	{
		this._canShoot = false;

		for (int i = 1; i <= _burstLength; i++)
		{
			ShootProjectile( direction );
			yield return new WaitForSecondsRealtime(_burstCooldown);
		}

		yield return new WaitForSecondsRealtime(shotCooldown);
		this._canShoot = true;
		this._lockBarrel = false;
	}
}
