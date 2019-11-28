using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy
{
	private void Start()
	{
		StartCoroutine("WatchOutPlayer");
	}

	private void Update()
	{
		if (_isChasingPlayer)
		{
			Vector2 PlayerOffset;
			PlayerOffset = _chasingPlayer.transform.position - transform.position;
			PlayerOffset.Normalize();
			transform.up = PlayerOffset;
		}
	}
}
