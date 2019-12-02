using System.Collections;
using UnityEngine;

public interface IShootable
{
	GameObject bulletPrefab
	{
		get;
		set;
	}

	float shotCooldown
	{
		get;
		set;
	}

	IEnumerator StartCooldown();
	void AttackPlayer();
}
