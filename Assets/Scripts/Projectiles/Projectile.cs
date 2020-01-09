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
            Player playerScript = buffer.GetComponent<Player>();
            playerScript.GetHit(_emitter, _damageAmount);
        }
    }
}
