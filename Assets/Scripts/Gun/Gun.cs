using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject _bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(Vector3 moveDirection = default(Vector3))
    {
        if (moveDirection == default(Vector3))
        {
            moveDirection = new Vector3(0, 0, 1);
        }

        GameObject obj = Instantiate(_bullet);
        obj.GetComponent<Projectile>()._moveDirection = moveDirection;
    }
}
