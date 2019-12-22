using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //The player object to follow
    [SerializeField]
    private GameObject _objectToFollow;

    //Magnitude needed to let the camera move
    [SerializeField]
    private float _frameValue = 5f;

    //Camera speed
    [SerializeField]
    private float _cameraSpeed = 0.15f;

    //Vector that will store the cumulated moves
    private Vector3 _addedMoves;

    private void Start()
    {
        //Initializing some values
        transform.position = new Vector3(_objectToFollow.transform.position.x, _objectToFollow.transform.position.y + 0.5f, -50);
        _addedMoves = new Vector3();
    }

    private void FixedUpdate()
    {
        //New position of the camera
        Vector3 newPosition = new Vector3(_objectToFollow.transform.position.x, _objectToFollow.transform.position.y + 0.5f, -50);
        //Vector to move to access the desired position
        Vector3 moveVector = newPosition - transform.position;
        //We store add this vector to the cumulated move vector
        _addedMoves += moveVector;

        //If the player asked to move with a magnitude superior to 5, the camera will start to follow the player
        if (_addedMoves.magnitude > _frameValue)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, _cameraSpeed);
        }

        //If the player stops moving, the cumulated moves vector is reset
        if (moveVector == Vector3.zero)
        {
            _addedMoves = new Vector3();
        }
    }
}
