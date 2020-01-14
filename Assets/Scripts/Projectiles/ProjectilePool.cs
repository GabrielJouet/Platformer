using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    //All projectiles in the scene but not used currently
    private List<Projectile> _allProjectiles = new List<Projectile>();

	[SerializeField]
	private List<GameObject> _projectileList = new List<GameObject>();

	//Method to get the Projectile component of a given id
	private GameObject GetProjectilePrefabById(int id)
	{
		GameObject buffer = null;

		foreach (GameObject projectilePrefab in _projectileList)
		{
			Projectile projectile = projectilePrefab.GetComponent<Projectile>();
			if (projectile.GetId() == id)
			{
				buffer = projectilePrefab;
			}
		}

		return buffer;
	}

	public Projectile UseProjectile(GameObject requester, int bulletId)
	{
		GameObject gameObjectBuffer = null;
		Projectile projectileBuffer = null;

		foreach (Projectile projectile in _allProjectiles)
		{
			if (bulletId == projectile.GetId())
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
			gameObjectBuffer = Instantiate(GetProjectilePrefabById(bulletId));
			gameObjectBuffer.SetActive(true);
			projectileBuffer = gameObjectBuffer.GetComponent<Projectile>();
		}

		projectileBuffer.SetEmitter(requester);
		return projectileBuffer;
	}

    //Method used when a projectile is used and its lifespan is finished
    public void AddProjectileToList(Projectile other)
    {
        other.gameObject.SetActive(false);
        _allProjectiles.Add(other);
    }
}
