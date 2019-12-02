using System.Collections;
using UnityEngine;

public interface IShootable
{
	int bulletId
	{
		get;
		set;
	}

	float shotCooldown
	{
		get;
		set;
	}
	float shotPrecision
	{
		get;
		set;
	}

	IEnumerator StartCooldown();
	void AttackPlayer();
}
