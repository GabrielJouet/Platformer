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
}
