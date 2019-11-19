using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
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
        transform.position = new Vector3(transform.position.x + _speed * Time.fixedDeltaTime, 0f, 0f);
    }


    public void Restart()
    {
        StartCoroutine(FadeOut());
    }


    private IEnumerator FadeOut()
    {
        yield return new WaitForSecondsRealtime(_livingTime);
        _projectilePool.AddProjectileToList(this);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
