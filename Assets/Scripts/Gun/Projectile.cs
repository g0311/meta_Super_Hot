using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 _moveDirection;
    public float _speed = 10;
    public bool _isGravity = false;
    public bool _isKillable = true;

    // Update is called once per frame
    void Update()
    {
        Vector3 prevPos = transform.localPosition; 
        prevPos.z += _speed * GameMode._instance._deltaTime;
        if(_isGravity)
        {
            prevPos.y -= 9.8f * GameMode._instance._deltaTime;
        }
        transform.localPosition = prevPos;
        Debug.Log("_speed : " + _speed + " _deltaTime : " + GameMode._instance._deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isKillable)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                GameMode._instance.PawnKilled(collision.gameObject);
                Destroy(this);
            }

        }
    }
}
