using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    //All projectiles in the scene but not used currently
    private List<Projectile> _allProjectiles = new List<Projectile>();



    //Method used when we have to recover or use a projectile
    public Projectile UseProjectile(Projectile other)
    {
        //We set an empty object to null
        Projectile buffer = null;

        //We will check every object already in the pool to know if the same projectile is already here
        foreach (Projectile current in _allProjectiles)
        {
            //If the projectile already exists
            if (other.gameObject.name + "(Clone)" == current.gameObject.name)
            {
                //We stop the for
                buffer = current;
                break;
            }
        }


        //If we find a projectile
        if(buffer != null)
        {
            //We set it active and remove it from list
            _allProjectiles.Remove(buffer);
            buffer.gameObject.SetActive(true);
        }
        //Else if we didn't find any projectile
        else
        {
            //We instantiate one
            buffer = Instantiate(other);
        }

        return buffer;
    }



    //Method used when a projectile is used and its lifespan is finished
    public void AddProjectileToList(Projectile other)
    {
        other.gameObject.SetActive(false);
        _allProjectiles.Add(other);
    }
}
