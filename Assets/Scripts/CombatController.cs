using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
	//The sprite renderer component of the player, it is used to know if the player is looking right or left
	private SpriteRenderer spriteRenderer;
	//Collider that will check if something is hit
	private BoxCollider2D hitCollider;

	private void Start()
	{
		spriteRenderer = GetComponentInParent<SpriteRenderer>();
		hitCollider = GetComponent<BoxCollider2D>();
		hitCollider.enabled = false;
	}

	private void Update()
	{
		if (Input.GetAxis("Fire1") > 0.5)
		{
			hitCollider.offset = new Vector2(IsGoingRight() * 0.1f, hitCollider.offset.y);
			StartCoroutine(TimeAttack());
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		collision.gameObject.SetActive(false);
	}

	private int IsGoingRight()
	{
		if (spriteRenderer.flipX)
		{
			return -1;
		}
		else
		{
			return 1;
		}
	}

	private IEnumerator TimeAttack()
	{
		hitCollider.enabled = true;
		yield return new WaitForSecondsRealtime(0.5f);
		hitCollider.enabled = false;
	}
}
