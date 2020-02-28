using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowCheck : MonoBehaviour
{
  Player _playerController;

  void Start()
  {
    _playerController = GetComponentInParent<Player>();
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    _playerController.AllowJump();
  }
}
