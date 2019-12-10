using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectToFollow;

    private void FixedUpdate()
    {
        transform.position = new Vector3(_objectToFollow.transform.position.x, _objectToFollow.transform.position.y + 0.5f, -10);
    }
}
