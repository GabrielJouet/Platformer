using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrappedPlateform : MonoBehaviour
{
    [SerializeField]
    private float _destroyTime = 1f;
    [SerializeField]
    private float _respawnTime = 3f;
    [SerializeField]
    private float _localXScale = 6.25f;
    [SerializeField]
    private float _localYScale = 0.5f;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;
    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(DestroyCoolDown());
        }
    }

    private IEnumerator DestroyCoolDown()
    {
        yield return new WaitForSeconds(_destroyTime);
        _spriteRenderer.enabled = false;
        _boxCollider.enabled = false;
        _rigidBody.simulated = false;
        StartCoroutine(RespawnCoolDown());
    }

    private IEnumerator RespawnCoolDown()
    {
        yield return new WaitForSeconds(_respawnTime);
        _spriteRenderer.enabled = true;
        _boxCollider.enabled = true;
        _rigidBody.simulated = true;
    }
}