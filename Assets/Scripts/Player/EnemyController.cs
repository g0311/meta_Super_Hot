using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Controller
{
    // Start is called before the first frame update
    public GameObject[] _armaturePool;
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
            _fracturePool[i].transform.position = _armaturePool[i].transform.position;
            _fracturePool[i].transform.rotation = _armaturePool[i].transform.rotation;
            _fracturePool[i].transform.localScale = _armaturePool[i].transform.localScale;
            _fracturePool[i].SetActive(true);
            _armaturePool[i].transform.gameObject.SetActive(false);
        }
        GetComponentInChildren<BoxCollider>().enabled = false;

        if(_weapon)
        {
            _weapon.transform.SetParent(null);
            _weapon.GetComponent<Projectile>().enabled = true;
            _weapon.GetComponent<Projectile>()._useGravity = true;
            _weapon.GetComponent<Rigidbody>().isKinematic = false;
            _weapon.GetComponent<BoxCollider>().enabled = true;
        }

        GetComponent<FSM>().enabled = false;
        this.enabled = false;
    }

    public void Move()
    {
        transform.Translate(Vector3.forward * _speed * GameMode.Instance._deltaTime);
        //animation speed
        GetComponentInChildren<Animator>().speed = _speed * GameMode.Instance._deltaTime * 90;
    }
    public void Rotate(Quaternion lookRotation)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, GameMode.Instance._deltaTime * 10);
    }
}
