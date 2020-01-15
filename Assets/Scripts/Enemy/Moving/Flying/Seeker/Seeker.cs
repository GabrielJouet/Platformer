using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : FlyingEnemy
{
	//Common function for all seeker projectiles. Here the object will track the player
	protected void FollowPlayer()
	{
		if (_chasingPlayer != null)
		{
			Vector2 playerCoordinate = _chasingPlayer.transform.position;
			Move(playerCoordinate.x, playerCoordinate.y);
		}
	}
}
