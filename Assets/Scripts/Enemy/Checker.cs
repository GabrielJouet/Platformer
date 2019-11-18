using UnityEngine;

public class Checker : MonoBehaviour
{
    //Did the checker is currently touching a wall?
    private bool _isTouchingWall;



    //Method triggered when a collision occurs
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the collision object is a wall
        _isTouchingWall = collision.gameObject.CompareTag("Wall");
    }



    //Method triggered when a collision is exited (a previous collision was done)
    private void OnTriggerExit2D(Collider2D collision)
    {
        //If the collision object is a wall
        _isTouchingWall = !collision.gameObject.CompareTag("Wall");
    }



    //Getter used for retrieving boolean isTouchingWall
    public bool GetIsTouchingWall()
    {
        return _isTouchingWall;
    }
}
