using UnityEngine;

public class Checker : MonoBehaviour
{
    private bool _isTouchingWall;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isTouchingWall = collision.gameObject.CompareTag("Wall");
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        _isTouchingWall = !collision.gameObject.CompareTag("Wall");
    }



    public bool GetIsTouchingWall()
    {
        return _isTouchingWall;
    }
}
