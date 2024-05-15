using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Controller
{
    // Start is called before the first frame update
    public GameObject[] _fracturePool;
    public GameObject _weapon;
    public int _fractureCount = 14;
    public float _speed;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public override void PawnDeath()
    {
        for(int i = 0; i < 14; i++)
        {
            Transform childTransform = transform.GetChild(i);
            _fracturePool[i].transform.position = childTransform.position;
            _fracturePool[i].transform.rotation = childTransform.rotation;
            _fracturePool[i].transform.localScale = childTransform.localScale;
            _fracturePool[i].SetActive(true);
            childTransform.gameObject.SetActive(false);
            //turn on the kinematic
        }
        if(!_weapon)
            _weapon.GetComponent<Rigidbody>().useGravity = true;
    }

    public void Move()
    {
        Transform curTr = transform;
        transform.localPosition *= _speed * GameMode._instance._deltaTime;
    }
    public void Rotate(Quaternion lookRotation)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, GameMode.Instance._deltaTime * 10);
    }
}
