using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Controller
{
    // Start is called before the first frame update
    public GameObject[] _fracturePool;
    public int _fractureCount = 14;
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
        }
    }    
}
