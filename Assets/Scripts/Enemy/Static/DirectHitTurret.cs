using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectHitTurret : Turret
{
	private void Start()
	{
		barrelTransform = transform.GetChild(0);
		projectilePool = FindObjectOfType<ProjectilePool>();

		StartCoroutine("WatchOutPlayer");
	}

	private void Update()
	{
		// If the watching coroutine detect that the turret is seeing the player
		if (_isChasingPlayer)
		{
			// The turret rotate to the direction of the player if the angle is small enough
			Vector2 PlayerOffset;

			PlayerOffset = _chasingPlayer.transform.position - transform.position;
			PlayerOffset.Normalize();

			if (Vector2.Angle(transform.up, PlayerOffset) < maxAngle)
			{
				barrelTransform.up = PlayerOffset;

				// And start to attack
				_isAttackingPlayer = true;

				if (canShoot)
				{
					AttackPlayer();
				}
			}
		}
	}
}
