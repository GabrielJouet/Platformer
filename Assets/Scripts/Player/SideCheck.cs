using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCheck : MonoBehaviour
{
  public int id;

  Player _playerController;

  void Start()
  {
    _playerController = GetComponentInParent<Player>();
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    _playerController.DeclareColliding(id, true);
  }

  void OnTriggerExit2D(Collider2D other)
  {
    _playerController.DeclareColliding(id, false);
  }
}
