using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    //All projectiles in the scene but not used currently
    private List<Projectile> _allProjectiles = new List<Projectile>();

	//Method used when we want a projectile, using a prefab template
	public Projectile UseProjectile(GameObject projectilePrefab)
	{
		GameObject gameObjectBuffer = null;
		Projectile projectileBuffer = null;

		foreach (Projectile projectile in _allProjectiles)
		{
			if (projectilePrefab.GetComponent<Projectile>().GetId() == projectile.GetId())
			{
				projectileBuffer = projectile;
			}
		}

		if (projectileBuffer != null)
		{
			_allProjectiles.Remove(projectileBuffer);
			projectileBuffer.gameObject.SetActive(true);
		}
		else
		{
			gameObjectBuffer = Instantiate(projectilePrefab);
			gameObjectBuffer.SetActive(true);
			projectileBuffer = gameObjectBuffer.GetComponent<Projectile>();
		}

		return projectileBuffer;
	}

    //Method used when a projectile is used and its lifespan is finished
    public void AddProjectileToList(Projectile other)
    {
        other.gameObject.SetActive(false);
        _allProjectiles.Add(other);
    }
}
