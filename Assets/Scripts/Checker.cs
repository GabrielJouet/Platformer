using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    [SerializeField]
    private int id;
    [SerializeField]
    private Player _player;

    public void OnTriggerEnter2D(Collider2D other)
    {
        {
            Debug.Log("collision");
            switch (id)
            {
                case 0:
                    _player.SetLowerBoxCollision(true);
                    break;
                case 1:
                    _player.SetUpperBoxCollision(true);
                    break;
                case 2:
                    _player.SetLeftBoxCollision(true);
                    break;
                case 3:
                    _player.SetRightBoxCollision(true);
                    break;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        {
            switch (id)
            {
                case 0:
                    _player.SetLowerBoxCollision(false);
                    break;
                case 1:
                    _player.SetUpperBoxCollision(false);
                    break;
                case 2:
                    _player.SetLeftBoxCollision(false);
                    break;
                case 3:
                    _player.SetRightBoxCollision(false);
                    break;
            }
        }
    }
}
