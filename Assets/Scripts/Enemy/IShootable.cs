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
	float shotRange
	{
		get;
		set;
	}
	ProjectilePool projectilePool
	{
		get;
		set;
	}

	IEnumerator StartCooldown();
	void AttackPlayer();
}
