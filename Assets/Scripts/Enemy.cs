using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField]
    protected float _fieldOfSight;


    [SerializeField]
    protected Transform _eyePosition;


    [SerializeField]
    protected float _minDistanceWithPlayer;


    protected GameObject _chasingPlayer;

    protected bool _isChasingPlayer;

    protected bool _isAttackingPlayer;



    protected IEnumerator WatchOutPlayer()
    {
        while (true)
        {
            GameObject buffer = GameObject.FindGameObjectWithTag("Player");

            if (buffer != null)
            {
                if (Mathf.Sqrt((buffer.transform.position - transform.position).sqrMagnitude) < _fieldOfSight)
                {
                    if (Physics2D.Linecast(_eyePosition.position, buffer.transform.position).collider == buffer.GetComponent<CircleCollider2D>())
                    {
                        _isChasingPlayer = true;
                        _chasingPlayer = buffer;
                    }
                    else
                    {
                        _isChasingPlayer = false;
                    }
                }
            }

            yield return new WaitForSecondsRealtime(0.2f);
        }
    }
}
