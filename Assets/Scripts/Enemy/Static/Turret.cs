using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy
{
	private Transform barrelTransform;
	private float maxAngle = 45.0f;

	private void Start()
	{
		barrelTransform = transform.GetChild(0);
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

				AttackPlayer();

			}
		}
	}

	private void AttackPlayer()
	{

	}
}
