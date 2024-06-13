using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Projectile : MonoBehaviour
{
    public Vector3 _moveDirection;
    public float _speed = 10;
    public bool _useGravity = false;
    public bool _isKillable = true;
    public ParticleSystem _hitParticle;
    public LayerMask _terrainMask;

    private float _gravity = -300.0f;
    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Ray ray = new Ray(transform.position, -Vector3.up);
            if (!Physics.Raycast(ray, 0.3f, _terrainMask))
            {
                if (!_useGravity)
                {
                    rb.velocity = _moveDirection * GameMode.Instance._moveDistance * _speed;
                }
                else
                {
                    _moveDirection += new Vector3(0, _gravity, 0);
                    rb.velocity = _moveDirection * GameMode.Instance._moveDistance * _speed;
                }
            }
            else
            {
                _moveDirection = Vector3.zero;
                rb.velocity = _moveDirection;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (_isKillable)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
            {
                if (_hitParticle)
                {
                    Vector3 contactPoint = transform.position;
                    Instantiate(_hitParticle, contactPoint, Quaternion.identity);
                }
                collision.gameObject.GetComponentInParent<Controller>().PawnDeath();
            }
            Destroy(gameObject);
        }
    }

    public void SetMoveDirection(Vector3 moveDirection)
    {
        _moveDirection = moveDirection;
    }
}
