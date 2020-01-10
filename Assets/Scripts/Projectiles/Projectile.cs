using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField]
	private int _id;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _livingTime;
    [SerializeField]
    private float _damageAmount;

    private ProjectilePool _projectilePool;
    private GameObject _emitter;

    private void Start()
    {
        _projectilePool = FindObjectOfType<ProjectilePool>();
		StartCoroutine(FadeOut());
    }

    private void FixedUpdate()
    {
		transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }
	public void Restart()
	{
		StartCoroutine(FadeOut());
	}

    public int GetId()
	{
		return this._id;
	}

    private IEnumerator FadeOut()
    {
        yield return new WaitForSecondsRealtime(_livingTime);
        _projectilePool.AddProjectileToList(this);
    }

    public void SetEmitter(GameObject emitter)
    {
        this._emitter = emitter;
    }

    //TODO collision with the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject buffer = collision.gameObject;

        if (buffer.tag == "Player")
        {
            Rigidbody2D playerRb2d = buffer.GetComponent<Rigidbody2D>();
            playerRb2d.AddForce(Vector2.up * 15000 + Vector2.right * transform.up.x * 10000);

            Player playerScript = buffer.GetComponent<Player>();
            playerScript.GetHit(_emitter, _damageAmount);
        }
        else
        {
            _projectilePool.AddProjectileToList(this);
        }
    }
}
