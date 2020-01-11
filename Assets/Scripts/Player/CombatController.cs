using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
	//The sprite renderer component of the player, it is used to know if the player is looking right or left
	private SpriteRenderer spriteRenderer;
	//Collider that will check if something is hit
	private BoxCollider2D hitCollider;
	//Animator
	private Animator animator;

	private bool canHit = true;

	private void Start()
	{
		spriteRenderer = GetComponentInParent<SpriteRenderer>();
		hitCollider = GetComponent<BoxCollider2D>();
		hitCollider.enabled = false;
		animator = GetComponentInParent<Animator>();
	}

	private void Update()
	{
		if (Input.GetAxis("Fire1") > 0.5 && canHit)
		{
			hitCollider.offset = new Vector2(IsGoingRight() * 0.15f, hitCollider.offset.y);
			animator.SetTrigger("hit");
			StartCoroutine(TimeAttack());
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		GameObject buffer = collision.gameObject;

		if (buffer.tag == "Enemy")
		{
			collision.gameObject.SetActive(false);
		}
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
		canHit = false;
		hitCollider.enabled = true;
		yield return new WaitForSecondsRealtime(.04f);
		hitCollider.enabled = false;
		yield return new WaitForSecondsRealtime(.16f);
		canHit = true;
	}
}
