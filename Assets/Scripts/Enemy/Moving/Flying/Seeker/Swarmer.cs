using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarmer : Seeker
{
	void Start()
	{
		StartCoroutine(WatchOutPlayer());
	}

	void FixedUpdate()
	{
		FollowPlayer();
	}

	new void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject buffer = collision.gameObject;

		if (buffer == _chasingPlayer)
		{
			buffer.GetComponent<Player>().GetHit(null, _damageAmount);
		}

		GetHit();
	}
}
