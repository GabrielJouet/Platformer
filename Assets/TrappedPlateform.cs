using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrappedPlateform : MonoBehaviour
{
    [SerializeField]
    private float _destroyTime = 1f; 
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
        Destroy(gameObject);
    }
}