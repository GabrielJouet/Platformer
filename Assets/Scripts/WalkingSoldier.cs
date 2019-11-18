using UnityEngine;

public class WalkingSoldier : WalkingEnemy
{
    private void Start()
    {
        _speedMax = _speed;

        StartCoroutine(WatchOutPlayer());
    }


    private void FixedUpdate()
    {
        if (_isChasingPlayer)
        {
            CheckPosition();


            if (Mathf.Sqrt((_chasingPlayer.transform.position - transform.position).sqrMagnitude) > _minDistanceWithPlayer + 0.01f)
            {
                _speed = _speedMax * 2f;
                Move(_chasingPlayer.transform.position);
            }
            else if (Mathf.Sqrt((_chasingPlayer.transform.position - transform.position).sqrMagnitude) <= _minDistanceWithPlayer - 0.01f)
            {
                _speed = _speedMax;
                _isAttackingPlayer = true;
                Move(FindRetreatPosition());
            }
        }
    }
}
