using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    protected float _health;

    protected float _healthMax;


    protected bool _isGrounded;



    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            _isGrounded = true;
        }
    }



    protected void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            _isGrounded = false;
        }
    }
}
