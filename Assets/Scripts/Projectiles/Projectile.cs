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

    private ProjectilePool _projectilePool;

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

	public void Restart(float livingTime, float speed)
    {
		this._livingTime = livingTime;
		this._speed = speed;
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

    //TODO collision with the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject buffer = collision.gameObject;

        if (buffer.tag == "Player")
        {
            buffer.SetActive(false);
        }
    }
}
