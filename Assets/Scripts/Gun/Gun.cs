using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject _bullet;
    public Transform _bulletStartTransform;
    public AudioSource _gunFireSound;

    public void Fire(Vector3 moveDirection = default)
    {
        _gunFireSound.Play();
        if (moveDirection == default)
        {
            moveDirection = new Vector3(0, 0, 1);
            moveDirection = transform.TransformDirection(moveDirection);
        }
        GameObject obj = Instantiate(_bullet);
        //set direction 
        obj.GetComponentInChildren<Projectile>().SetMoveDirection(moveDirection);
        //set transform
        obj.transform.position = _bulletStartTransform.position;
        obj.transform.rotation = transform.rotation;
    }
}
