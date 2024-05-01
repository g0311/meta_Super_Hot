using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject _bullet;
    public int _bulletCount = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        if (_bulletCount-- != 0)
        {
            Instantiate(_bullet);
            _bullet.GetComponent<Projectile>()._moveDirection = Vector3.forward;
        }
    }
}
