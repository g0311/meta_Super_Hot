using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 _moveDirection;
    public float _speed = 10;
    public bool _useGravity = false;
    public bool _isKillable = true;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GameMode.Instance._deltaTime);
        Rigidbody rb = GetComponent<Rigidbody>();
        if (!_useGravity)
        {
            rb.velocity = _moveDirection * GameMode.Instance._deltaTime * _speed;
        }
        else
        {
            _moveDirection += new Vector3(0, -7.0f, 0);
            rb.velocity = _moveDirection * GameMode.Instance._deltaTime * _speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isKillable)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
            {
                GameMode._instance.PawnKilled(collision.gameObject);
                _isKillable = false;
            }

        }
    }

    public void SetMoveDirection(Vector3 moveDirection)
    {
        _moveDirection = moveDirection;
    }
}
