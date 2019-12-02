using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    //The maximum distance the enemy can see the player
    [SerializeField]
    protected float _fieldOfSight;


    //The position of eye, camera or any observatory chip (used for seeing player)
    [SerializeField]
    protected Transform _eyePosition;


    //The minimum distance the enemy will try to keep with the player
    [SerializeField]
    protected float _minDistanceWithPlayer;


    //The actual player
    protected GameObject _chasingPlayer;

    //Did the enemy is actually chasing the player?
    protected bool _isChasingPlayer;

    //Did the enemy is actually trying to kill the player?
    protected bool _isAttackingPlayer;



    //Method used in order to checking if the player is in sight or not
    protected IEnumerator WatchOutPlayer()
    {
        //Make it forever
        while (true)
        {
            //We find the player
            GameObject buffer = GameObject.FindGameObjectWithTag("Player");

            //If the player exists (not dead)
            if (buffer != null)
            {
				int layerMasks = LayerMask.GetMask("Wall");

				//If the distance with the player is less than field of sight
				if (Mathf.Sqrt((buffer.transform.position - transform.position).sqrMagnitude) < _fieldOfSight
					&& (Physics2D.Linecast(_eyePosition.position, buffer.transform.position, layerMasks).collider == buffer.GetComponent<CircleCollider2D>()))
                {
					//The enemy chases it down
					_isChasingPlayer = true;
					_chasingPlayer = buffer;
				}
				else
				{
					//The enemy leaves the player
					_isChasingPlayer = false;
					_chasingPlayer = null;
                }
            }

            //We wait a certain amount of time before retrying
            yield return new WaitForSecondsRealtime(0.2f);
        }
    }
}
