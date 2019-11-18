using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _speed = 12;
    [SerializeField]
    private int _gravityScale = 25;
    [SerializeField]
    private Rigidbody2D _rigidBody;

    private bool _isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector2 horizontalDirection = new Vector2(horizontalInput, 0);

        transform.Translate(horizontalDirection * Time.deltaTime * _speed);

        if (Input.GetKeyDown(KeyCode.Space) && _isJumping == false)
        {
            _isJumping = true;
            Jump();
        }
    }

    private void Jump()
    {
        _rigidBody.AddForceAtPosition(transform.up * _gravityScale * _speed, transform.position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            _isJumping = false;
        }
    }
}

