using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectToFollow;

    private Vector3 _addedMoves;

    private void Start()
    {
        transform.position = new Vector3(_objectToFollow.transform.position.x, _objectToFollow.transform.position.y + 0.5f, -50);
        _addedMoves = new Vector3();
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(_objectToFollow.transform.position.x, _objectToFollow.transform.position.y + 0.5f, -50);
        Vector3 moveVector = newPosition - transform.position;
        _addedMoves += moveVector;

        if (_addedMoves.magnitude > 5)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, 0.1f);
        }

        if (moveVector == Vector3.zero)
        {
            _addedMoves = new Vector3();
        }
    }
}
