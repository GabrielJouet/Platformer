using UnityEngine;

public class Entity : MonoBehaviour
{
    //The current health of the entity
    [SerializeField]
    protected float _health;

    //The maximum health the entity can have
    protected float _healthMax;


    //Did the entity hit the ground?
    protected bool _isGrounded;



    //Method triggered when a collision occurs
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        //If the collision object is a wall
        _isGrounded = collision.gameObject.CompareTag("Wall");
    }



    //Method triggered when a collision is exited (a previous collision was done)
    protected void OnCollisionExit2D(Collision2D collision)
    {
        //If the collision object is a wall
        _isGrounded = !collision.gameObject.CompareTag("Wall");
    }



    //Method used when we get hit by something (damage given)
    public void GetHit(float damage)
    {
        _health -= damage;

        //If we don't have health anymore
        if(_health <= 0f)
        {
            //We destroy the object
            Destroy(gameObject);
        }
    }



    //Method used when we get hit (damage not given)
    public void GetHit()
    {
        //We destroy the object
        Destroy(gameObject);
    }
}
