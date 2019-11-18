using UnityEngine;

public class Checker : MonoBehaviour
{
    private bool _isTouchingWall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _isTouchingWall = true;
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _isTouchingWall = false;
        }
    }


    public bool GetIsTouchingWall()
    {
        return _isTouchingWall;
    }
}
