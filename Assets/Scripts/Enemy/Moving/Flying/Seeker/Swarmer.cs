using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarmer : Seeker
{
	private bool _isHiding = true;
	private SpriteRenderer _spriteRenderer;

	void Start()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		StartCoroutine(WatchOutPlayer());
	}

	void FixedUpdate()
	{
		if (_chasingPlayer != null)
		{
			_isHiding = false;
			FollowPlayer();
		}
		else if (!_stillRememberPlayer)
		{
			_isHiding = true;
		}

		if (_isHiding && _spriteRenderer.enabled)
		{
			_spriteRenderer.enabled = false;
		}
		if (!_isHiding && !_spriteRenderer.enabled)
		{
			_spriteRenderer.enabled = true;
		}
	}

	new void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject buffer = collision.gameObject;

		if (buffer == _chasingPlayer)
		{
			buffer.GetComponent<Player>().GetHit(null, _damageAmount);
		}

		GetHit();
	}
}
