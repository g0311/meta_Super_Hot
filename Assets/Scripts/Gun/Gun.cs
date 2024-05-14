using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject _bullet;
    public Transform _bulletStartTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(Vector3 moveDirection = default)
    {
        if (moveDirection == default)
        {
            moveDirection = new Vector3(0, 0, 1);
        }
        GameObject obj = Instantiate(_bullet);
        //set transform
        obj.transform.position = _bulletStartTransform.position;
        obj.transform.rotation = transform.rotation;
        //set direction 
        obj.GetComponentInChildren<Projectile>()._moveDirection = moveDirection;
    }
}
