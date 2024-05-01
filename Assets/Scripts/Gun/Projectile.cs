using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 _moveDirection;
    public float _speed;

    // Update is called once per frame
    void Update()
    {
        transform.position = _moveDirection * _speed * GameMode._instance._deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameMode._instance.PawnKilled(collision.gameObject);
        }
        Destroy(this);
    }
}
